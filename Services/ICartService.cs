using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface ICartService
{
    public Task<CartItem?> AddProductToCartAsync(CartDTO request, string userId);
    public Task<CartItem?> RemoveProductFromCartAsync(CartDTO request, string userId);
    public Task<ICollection<CartDTO>> GetItemsInCartAsync(string userId);
}