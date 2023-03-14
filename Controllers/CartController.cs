using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[Authorize]
[Route("api/v1/my-cart")]
public class CartController : ApiControllerBase
{

    private readonly ICartService _service;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService service, ILogger<CartController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("add")]
    public async Task<ActionResult<CartDTO>> AddToCart(CartDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();

        var carItem = await _service.AddProductToCartAsync(request, userId);
        if(carItem is null)
            return BadRequest();

        return new CartDTO
        {
            ProductId = carItem.ProductId,
            Count = carItem.Count
        };
    }

    [HttpPost("remove")]
    public async Task<ActionResult<CartDTO>> RemoveFromCart(CartDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();

        var carItem = await _service.RemoveProductFromCartAsync(request, userId);
        if(carItem is null)
            return BadRequest();

        return new CartDTO
        {
            ProductId = carItem.ProductId,
            Count = carItem.Count
        };
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<CartDTO>>> GetCartItems()
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();

        return Ok(await _service.GetItemsInCartAsync(userId));
    }
}