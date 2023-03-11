namespace Ecommerce.Models;

public class CartItem
{
    public User User {get; set;} = null!;
    public int UserId {get; set;}
    public Product Product {get; set;} = null!;
    public int ProductId {get; set;}
    public int Count {get; set;} = 0;
}