namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface IProductSurvice : ICrudService<Product, ProductDTO>, ISearchForignID
{
    Task<ICollection<Product>> GetAllByAsync( int offset, int limit, int? categoryId,
        string? priceSort, string? titleSort, string? searchString);

    Task<ICollection<Review>?> GetReviewsAsync(int id);
}