namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    // Product controller with search queries does the same work!
    // Could remove it completely or Add offset/Limit in the future!
    [HttpGet("{id:int}/products")]
    [AllowAnonymous]
    public async Task<ICollection<Product>> GetProducts(int id)
    {
        return await _service.GetProductsAsync(id);
    }
}