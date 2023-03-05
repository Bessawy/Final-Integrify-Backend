namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using Microsoft.EntityFrameworkCore;

public class DbCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
 
    protected readonly AppDbContext _dbContext;

    public DbCrudService(AppDbContext dbContext) => _dbContext = dbContext;

    public virtual async Task<TModel?> CreateAsync(TDto request)
    {
        var item = new TModel();
        if(request.CreateModel(item))
        {
            _dbContext.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        if(item is null)
            return false;
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return await _dbContext.Set<TModel>().ToListAsync();
    }

    public async Task<TModel?> GetAsync(int id)
    {
        return await _dbContext.Set<TModel>().FindAsync(id);
    }

    public virtual async Task<TModel?> UpdateAsync(int id, TDto request)
    {
        var item = await GetAsync(id);
        if(item is null)
            return null;
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}