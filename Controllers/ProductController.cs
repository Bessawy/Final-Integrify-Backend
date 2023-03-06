namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Common;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public class ProductController : CrudController<Product, ProductDTO>
{
    private readonly ILogger<UserController> _logger;
    public ProductController(ICrudService<Product, ProductDTO> service,
        ILogger<UserController> logger) : base(service)
    {
        _logger = logger;
    }
}