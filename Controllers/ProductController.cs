namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ProductController : CrudController<Product, ProductDTO>
{
    private readonly ILogger<UserController> _logger;
    private readonly IProductSurvice _service;

    public ProductController(IProductSurvice service,
        ILogger<UserController> logger) : base(service)
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
}