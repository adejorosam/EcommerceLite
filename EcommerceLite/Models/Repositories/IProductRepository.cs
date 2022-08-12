using EcommerceLite.Models.Domain;
namespace EcommerceLite.Models.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);

        Task<Product> GetAsync(Guid id);

        Task<Product> AddAsync(Product product);

        Task<Product> DeleteAsync(Guid id);

        Task<Product> UpdateAsync(Guid id, Product product);
    }
}