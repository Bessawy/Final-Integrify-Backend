namespace Ecommerce.Models;

public class User : BaseModel
{

    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;
    public string? Avatar {get; set;}
    public UserRole Role {get; init;} 

    public enum UserRole
    {
        Admin,
        Customer
    }

}