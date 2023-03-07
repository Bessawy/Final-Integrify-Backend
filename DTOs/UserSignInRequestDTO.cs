namespace Ecommerce.DTOs;

using System.ComponentModel.DataAnnotations;

public class UserSignInRequestDTO
{
    [EmailAddress]
    public string Email {get; set;} = null!;
    
    public string Password {get; set;} = null!;
}