namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface IProductSurvice : ICrudService<Product, ProductDTO>, ISearchForignID
{
}