using EcommerceLite.Models.Domain;

namespace EcommerceLite.Models.Repositories
{
    public interface ICartRepository
    {
        //Task<IEnumerable<Cart>> GetAllAsync();

        Task<Cart> GetAsync(Guid id);

        Task<Cart> AddAsync(Cart cart);

        Task<Cart> DeleteAsync(Guid id);

        Task<Cart> UpdateAsync(Guid id, Cart cart);
    }
}