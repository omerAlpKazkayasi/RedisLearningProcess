using RedisOrnekAPI.Models;

namespace RedisOrnekAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
    }
}
