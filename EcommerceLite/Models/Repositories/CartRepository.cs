using System;
using EcommerceLite.Models.Domain;
using EcommerceLite.Data;

namespace EcommerceLite.Models.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;


        public CartRepository(EcommerceLiteDbContext ecommerceLiteDbContext)
        {
            this.ecommerceLiteDbContext = ecommerceLiteDbContext;
        }

        public async Task<Cart> AddAsync(Cart cart)
        {
            cart.Id = Guid.NewGuid();
            await ecommerceLiteDbContext.AddAsync(cart);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return cart;

        }
    }
}

