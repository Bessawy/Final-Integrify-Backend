namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using System.Collections.Generic;

public class DbCategoryService : DbCrudService<Category, CategoryDTO>, ICategorySurvice
{
    public DbCategoryService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Product>> GetProductsAsync(int id)
    {
        Category? category = await GetAsync(id);
        if(category is null)
            return new List<Product>();
    
        await _dbContext.Entry(category).Collection(c=> c.Products).LoadAsync();
        return category.Products;
    }
}