namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DbCategoryService : DbCrudService<Category, CategoryDTO>, ICategorySurvice
{
    
    public DbCategoryService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Product>> GetProductsAsync(int id, int offset, int limit)
    {
        Category? category = await GetAsync(id);
        if(category is null)
            return new List<Product>();
  
        return await _dbContext.Entry(category)
            .Collection(c=> c.Products)
            .Query()
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
}