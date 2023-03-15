namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface ICategorySurvice : ICrudService<Category, CategoryDTO>
{
    Task<ICollection<Product>> GetProductsAsync(int id, int offset, int limit); 
}