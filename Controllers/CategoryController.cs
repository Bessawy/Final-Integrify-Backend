namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/categories")]
public class CategoryController : CrudController<Category, CategoryDTO>
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategorySurvice _service;

    public CategoryController(ICategorySurvice service,
        ILogger<CategoryController> logger) : base(service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{id:int}/products")]
    public async Task<ICollection<Product>> GetProducts(int id)
    {
        return await _service.GetProductsAsync(id);
    }
}