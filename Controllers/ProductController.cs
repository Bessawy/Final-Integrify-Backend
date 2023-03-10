namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Policy = "admin")]
    public override async Task<ActionResult<Product?>> Update(int id, ProductDTO request)
    {
        bool res = await _service.IsForignIDValidAsync(request.CategoryId);
        if(!res)   
            return NotFound("CategoryId not Found");
        return await base.Update(id, request);
    }
}