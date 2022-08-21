using System;
using EcommerceLite.Models.Domain;
using EcommerceLite.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLite.Models.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;


        public CartRepository(EcommerceLiteDbContext ecommerceLiteDbContext)
        {
            this.ecommerceLiteDbContext = ecommerceLiteDbContext;
        }

        public async Task<Cart_Product> AddCartProductAsync(Cart_Product cartProduct)
        {
            await ecommerceLiteDbContext.AddAsync(cartProduct);

            await ecommerceLiteDbContext.SaveChangesAsync();

            return cartProduct;
        }


        public async Task<Cart> AddAsync(Cart cart)
        {
            //cart.Id = Guid.NewGuid();

            await ecommerceLiteDbContext.AddAsync(cart);

            await ecommerceLiteDbContext.SaveChangesAsync();

            return cart;

        }

        public async Task<Cart> UpdateAsync(Guid? id, Cart cart)
        {
            var existingCart = await ecommerceLiteDbContext.Carts.FindAsync(id);

            if (existingCart == null)
            {
                return null;
            }

            existingCart.Quantity = cart.Quantity;
            existingCart.TotalPrice = cart.TotalPrice;

            await ecommerceLiteDbContext.SaveChangesAsync();

            return existingCart;

        }

       

        public async Task<Cart> GetAsync(Guid? id)
        {

            var cart = await ecommerceLiteDbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);

            return cart;

        }
        //var fromDate = DateTime.Now.AddDays(-7);

        //var customer = context.Customers.Where(c => c.CustomerID == 1)
        //    .Include(c => c.Invoices)
        //    .Where(c => c.Invoices.Any(i => i.Date >= fromDate))
        //    .FirstOrDefault();

        public async Task<IEnumerable<Cart_Product>> GetCartProductAsync(Guid? cartId, Guid productId)
        {

            var cartProduct = await ecommerceLiteDbContext.CartProducts
                .Where(c => c.CartId == cartId)
                .Where(d => d.ProductId == productId)
                .ToListAsync();
                //FirstOrDefaultAsync(x => x.Id == id);

            return cartProduct;

        }

        public async Task<Cart> DeleteAsync(Guid id)
        {
            var cart = await ecommerceLiteDbContext.Carts.FindAsync(id);

            if (cart == null)
            {
                return null;
            }

            //Delete the Cart
            ecommerceLiteDbContext.Carts.Remove(cart);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return cart;

        }
    }
}