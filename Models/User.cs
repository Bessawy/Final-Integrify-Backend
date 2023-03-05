namespace Ecommerce.Models;

using System.ComponentModel.DataAnnotations;

public class User : BaseModel
{

    [MaxLength(256)]
    public string Name {get; set;} = null!;
    [MaxLength(256)]
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