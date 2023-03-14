namespace Ecommerce.DTOs;

using Ecommerce.Models;

public class CartDTO
{
    public Product? Product {get; set;}
    public int ProductId {get; set;}
    public int Count {get; set;}

     public static CartDTO FromCart(CartItem cart)
    {
        return new CartDTO
        {
            Product = cart.Product,
            Count = cart.Count
        };
    }
}