namespace Ecommerce.DTOs;

using Ecommerce.Models;
using Ecommerce.Common;
using System.ComponentModel.DataAnnotations;

public class UserDTO : BaseDTO<User>
{
    [MaxLength(3, ErrorMessage = "Given name is have less than 3 characters!")]    
    public string? Name {get; set;}

    [EmailAddress(ErrorMessage = "Given email is not valid!")]
    public string? Email {get; set;}
    
    [Password(ErrorMessage = @"Password entered is not valid, 
        please review given constraints!")] 
    public string? Password {get; set;}    
    public string? Avatar {get; set;}

    public override void UpdateModel(User model)
    {
        model.Name = Name ?? model.Name;
        model.Email = Email ?? model.Email;
        model.Password = Password ?? model.Password;
        model.Avatar = Avatar ?? model.Avatar;
        model.UpdateAt = DateTime.Now;      
    }
}