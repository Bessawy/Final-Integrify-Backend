namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class CartService : ICartService
{
    private readonly AppDbContext _dbContext;

    public CartService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CartItem?> AddProductToCartAsync(int id, User user)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if(product == null)
            return null;
        
        await _dbContext.Entry(user).Collection(u => u.Carts).LoadAsync();
        var cartItem = user.Carts.SingleOrDefault(c => c.ProductId == product.Id);
        
        if(cartItem is null)
        {
            cartItem = new CartItem
            {
                Product = product,
                User = user,
                Count = 1
            };

            user.Carts.Add(cartItem);
        }
        else
        {
            cartItem.Count++;
        }

        await _dbContext.SaveChangesAsync();
        return cartItem;
    }
}  