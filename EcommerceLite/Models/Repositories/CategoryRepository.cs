using Microsoft.EntityFrameworkCore;
using EcommerceLite.Data;
using EcommerceLite.Models.Domain;

namespace EcommerceLite.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;


        public CategoryRepository(EcommerceLiteDbContext ecommerceLiteDbContext)
        {
            this.ecommerceLiteDbContext = ecommerceLiteDbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return
                await ecommerceLiteDbContext.Categories
                .ToListAsync();
        }

        public async Task<Category> GetAsync(Guid id)
        {
            var category = await
                ecommerceLiteDbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            await ecommerceLiteDbContext.AddAsync(category);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return category;

        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await ecommerceLiteDbContext.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Description = category.Description;
            existingCategory.Name = category.Name;

            await ecommerceLiteDbContext.SaveChangesAsync();

            return existingCategory;

        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            var category = await ecommerceLiteDbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            //Delete the Category
            ecommerceLiteDbContext.Categories.Remove(category);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return category;

        }

    }
}

