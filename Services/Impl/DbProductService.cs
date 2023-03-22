namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DbProductService : DbCrudService<Product, ProductDTO>, IProductSurvice
{
    
    public DbProductService(AppDbContext context) : base(context)
    {
    }

    public override async Task<ICollection<Product>> GetAllAsync(int offset, int limit)
    {
        return await _dbContext.Set<Product>()
            .AsNoTracking()
            .Include(p => p.Category)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
    
    public override async Task<Product?> GetAsync(int id)
    {
        return await _dbContext.Set<Product>()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<Product>> GetAllByAsync(int offset, int limit, int? categoryId,
            string? priceSort, string? titleSort, string? searchString)
    {
        var products = _dbContext.Products.Select(p => p)
            .AsNoTracking();
            
        if(!String.IsNullOrEmpty(searchString))
            products = products.Where(p => p.Title.ToLower()
                .Contains(searchString.ToLower()));

        if(categoryId is not null)
            products = products.Where(p => p.CategoryId == categoryId);

        if(!String.IsNullOrEmpty(priceSort))
        {
            if(priceSort == "desc")
                products = products.OrderByDescending(p => p.Price);
            else
                products = products.OrderBy(p => p.Price);
        }

        if(!String.IsNullOrEmpty(titleSort))
        {
            if(titleSort == "desc")
                products = products.OrderByDescending(p => p.Title);
            else
                products = products.OrderBy(p => p.Title);
        }

        return await products
            .Include(p => p.Category)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<bool> IsForignIDValidAsync(int? id)
    {
        if(id is null)
            return true;
            
        Category? category = await _dbContext.Categories.FindAsync(id);
        if(category is null)
            return false;
        return true;
    }

    public async Task<ICollection<ReviewDTO>?> GetReviewsAsync(int id, int offset, int limit)
    {
        Product? product = await GetAsync(id);
        if(product is null)
            return null;
        
        return await _dbContext.Entry(product).Collection(p => p.Reviews)
            .Query()
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .Include(r => r.User)
            .Select(r => ReviewDTO.FromReview(r))
            .ToListAsync();
    }
}