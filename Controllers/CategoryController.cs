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
    // Could remove it completely!
    [HttpGet("{id:int}/products")]
    [AllowAnonymous]
    public async Task<ICollection<Product>> GetProducts(int id, int offset = 0, int limit = 100)
    {
        return await _service.GetProductsAsync(id, offset, limit);
    }

    // Override defualt autherization to only admins
    [Authorize(Policy = "admin")]
    public override async Task<IActionResult> Delete(int id)
    {
        return await base.Delete(id);
    }

    // Override defualt autherization to only admins
    [Authorize(Policy = "admin")]
    public override async Task<ActionResult<Category?>> Update(int id, CategoryDTO request)
    {
        return await base.Update(id, request);
    }
}