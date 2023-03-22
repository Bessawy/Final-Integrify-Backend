namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _service;

    public CartService(AppDbContext dbContext, IUserService service)
    {
        _dbContext = dbContext;
        _service = service;
    }
    
    public async Task<ICollection<CartDTO>?> GetItemsInCartAsync(string userId)
    {
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return null;

        await _dbContext.Entry(user).Collection(u => u.Carts).LoadAsync();

        // Items should be few in the cart
        foreach (var item in user.Carts)
            await _dbContext.Entry(item).Reference(i => i.Product).LoadAsync();

        return user.Carts.Select(c => CartDTO.FromCart(c)).ToList();
    }

    // Allow user to Add item to his/her Cart.
    public async Task<CartItem?> AddProductToCartAsync(CartDTO request, string userId)
    {
        (CartItem? cartItem, User user, Product product)? res 
            = await GetCartItemAsync(request, userId);

        if(res == null)
            return null;

        var cartItem = res.Value.cartItem;
        var user = res.Value.user;
        var product = res.Value.product;

        // If cartItem not found create a new one, 
        // else add to the count!
        if(cartItem is null)
        {
            cartItem = new CartItem
            {
                Product = product,
                User = user,
                Count = request.Count
            };
            user.Carts.Add(cartItem);
        }
        else
        {
            cartItem.Count += request.Count;
        }

        await _dbContext.SaveChangesAsync();
        return cartItem;
    }

    // Allow user to remove item from his/her Cart.
    public async Task<CartItem?> RemoveProductFromCartAsync(CartDTO request, string userId)
    {
        (CartItem? cartItem, User user, Product product)? res 
            = await GetCartItemAsync(request, userId);

        if(res == null)
            return null;

        var cartItem = res.Value.cartItem;
        var user = res.Value.user;
        var product = res.Value.product;

        if(cartItem is null)
        {
            return null;
        }
        else
        {
            // Reduce the number of items or remove it completely if the count
            // is less than zero.
            if(cartItem.Count - request.Count > 1)
            {
                cartItem.Count -= request.Count;
            }
            else
            {
                user.Carts.Remove(cartItem);
                cartItem.Count = 0;
            }
        }

        await _dbContext.SaveChangesAsync();
        return cartItem;
    }

    // Returns user, product and their corresponding CartItem if exist.
    public async Task<(CartItem?, User, Product)?> GetCartItemAsync(CartDTO request, string userId)
    {
        var user = await _service.FindUserByIdAsync(userId);
        var product = await _dbContext.Products.FindAsync(request.ProductId);

        if(product == null || user == null)
            return null;

        await _dbContext.Entry(user).Collection(u => u.Carts).LoadAsync();
        var cartItem = user.Carts.SingleOrDefault(c => c.ProductId == product.Id);

        return (cartItem, user, product); 
    }
}  