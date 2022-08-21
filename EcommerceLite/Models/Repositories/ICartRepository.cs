using EcommerceLite.Models.Domain;

namespace EcommerceLite.Models.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetAsync(Guid? id);

        Task<Cart> AddAsync(Cart cart);

        Task<Cart> DeleteAsync(Guid id);

        Task<Cart> UpdateAsync(Guid? id, Cart cart);

        Task<IEnumerable<Cart_Product>> GetCartProductAsync(Guid? cartId, Guid productId);

        Task<Cart_Product> AddCartProductAsync(Cart_Product cartProduct);
    }
}