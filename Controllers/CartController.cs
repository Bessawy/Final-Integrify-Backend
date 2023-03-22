using Ecommerce.DTOs;
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

    [HttpPost("add-item")]
    public async Task<ActionResult<CartDTO>> AddToCart(CartDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        var carItem = await _service.AddProductToCartAsync(request, userId);
        if(carItem is null)
            return NotFound();

        return new CartDTO
        {
            ProductId = carItem.ProductId,
            Count = carItem.Count
        };
    }

    [HttpPost("remove-item")]
    public async Task<ActionResult<CartDTO>> RemoveFromCart(CartDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        var carItem = await _service.RemoveProductFromCartAsync(request, userId);
        if(carItem is null)
            return NotFound();

        return new CartDTO
        {
            ProductId = carItem.ProductId,
            Count = carItem.Count
        };
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<CartDTO>>> GetCartItems()
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        var items = await _service.GetItemsInCartAsync(userId);
        if(items is null)
            return NotFound();
        return Ok(items);
    }
}