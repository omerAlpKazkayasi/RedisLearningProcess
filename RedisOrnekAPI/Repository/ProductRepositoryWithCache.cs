using RedisExample;
using RedisOrnekAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisOrnekAPI.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private const string productKey= "productCaches";
        private readonly IProductRepository _repository;
        private readonly RedisService   _redisService;
        private readonly IDatabase cacheRepository;

        public ProductRepositoryWithCache(RedisService redisService, IProductRepository repository)
        {
            _redisService = redisService;
            _repository = repository;
            cacheRepository = redisService.GetDb(0);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct=await _repository.CreateAsync(product);
            if (await cacheRepository.KeyExistsAsync(productKey))
            {
                await cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(newProduct));
            }
            return newProduct;
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await cacheRepository.KeyExistsAsync(productKey))
                return await LoadToCacheFromDbAsync();
            var products=new List<Product>();
            var cacheProducts = await cacheRepository.HashGetAllAsync(productKey);
            foreach (var item in cacheProducts.ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);  
            }
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (await cacheRepository.KeyExistsAsync(productKey))
            {
                var product =await cacheRepository.HashGetAsync(productKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }
            var products = LoadToCacheFromDbAsync();
            return products.Result.FirstOrDefault(x=>x.Id==id);  



        }
        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products=await _repository.GetAsync();
            products.ForEach(product =>
            {
                cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(product));
            });
            return products;
        }
    }
}
