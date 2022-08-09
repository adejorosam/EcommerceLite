using EcommerceLite.Models.Domain;

namespace EcommerceLite.Models.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetAsync(Guid id);

        Task<Category> AddAsync(Category category);

        Task<Category> DeleteAsync(Guid id);

        Task<Category> UpdateAsync(Guid id, Category category);
    }
}

