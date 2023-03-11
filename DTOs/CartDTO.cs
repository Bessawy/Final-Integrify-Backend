namespace Ecommerce.DTOs;

using Ecommerce.Models;

public class CartDTO
{
    public Product Product {get; set;} = null!;
    public int Count {get; set;}
}