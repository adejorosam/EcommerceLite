using System;
using EcommerceLite.Data;
using EcommerceLite.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLite.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;

        public ProductRepository(EcommerceLiteDbContext ecommerceLiteDbContext)
        {
            this.ecommerceLiteDbContext = ecommerceLiteDbContext;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await ecommerceLiteDbContext.Products
                    .Include(x => x.Category)
                    .Include(x => x.User)
                    .ToListAsync();
        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            var product = await ecommerceLiteDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            //Delete the Category
            ecommerceLiteDbContext.Products.Remove(product);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return product;

        }

        public async Task<Product> GetAsync(Guid id)
        {
           
            var product = await
                ecommerceLiteDbContext.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;

        }

        public async Task<Product> AddAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            await ecommerceLiteDbContext.AddAsync(product);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return product;

        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await ecommerceLiteDbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;
            existingProduct.SellingPrice = product.SellingPrice;
            existingProduct.CostPrice = product.CostPrice;
            existingProduct.AvailableQuantity = product.AvailableQuantity;
            existingProduct.ExpirationDate = product.ExpirationDate;
            existingProduct.Sku = product.Sku;

            await ecommerceLiteDbContext.SaveChangesAsync();

            return existingProduct;

        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var product = await ecommerceLiteDbContext.Products.Where(s => s.CategoryId == categoryId).ToListAsync();

            return product;
        }
    }
}

