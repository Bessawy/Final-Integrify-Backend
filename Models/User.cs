namespace Ecommerce.Models;

public class User : BaseModel
{
    public string Name {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string Avatar {get; set;}
    public UserRole Role {get; init;}

    public enum UserRole
    {
        Admin,
        Customer
    }

}