namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public class ProductController : CrudController<Product, ProductDTO>
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductSurvice _service;

    public ProductController(IProductSurvice service,
        ILogger<ProductController> logger) : base(service)
    {
        _logger = logger;
        _service = service;
    }

    public override async Task<ActionResult<Product?>> Create(ProductDTO request)
    {   
        bool res = await _service.IsForignIDValidAsync(request.CategoryId);
        if(!res)   
            return NotFound("CategoryId not Found");
        return await base.Create(request);
    }

    public override async Task<ActionResult<Product?>> Update(int id, ProductDTO request)
    {
        bool res = await _service.IsForignIDValidAsync(request.CategoryId);
        if(!res)   
            return NotFound("CategoryId not Found");
        return await base.Update(id, request);
    }

    public override async Task<IActionResult> Delete(int id)
    {
        return await base.Delete(id);
    }

    [Route("search")]
    public async Task<ICollection<Product>> GetAllBy([FromQuery] int? id, 
        [FromQuery] string? price,
        [FromQuery] string? title, 
        [FromQuery] string? search, 
        [FromQuery] int offset = 0, 
        [FromQuery]int limit = 100)
    {
        return await _service.GetAllByAsync(offset, limit, id, price, title, search);
    }
}