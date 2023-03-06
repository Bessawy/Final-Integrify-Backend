namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DbProductService : DbCrudService<Product, ProductDTO>
{
    public DbProductService(AppDbContext context) : base(context)
    {
    }

    public override async Task<ICollection<Product>> GetAllAsync()
    {
        return await _dbContext.Set<Product>()
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();
    }
    
    public override async Task<Product?> GetAsync(int id)
    {
        return await _dbContext.Set<Product>()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
 
}