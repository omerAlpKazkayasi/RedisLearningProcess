using RedisOrnekAPI.Models;
using RedisOrnekAPI.Repository;

namespace RedisOrnekAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<Product> CreateAsync(Product product)
        {
            return _repository.CreateAsync(product);
        }

        public Task<List<Product>> GetAsync()
        {
            return _repository.GetAsync();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
    }
}
