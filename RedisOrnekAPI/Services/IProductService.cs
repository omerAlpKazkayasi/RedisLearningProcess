using RedisOrnekAPI.Models;

namespace RedisOrnekAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
    }
}
