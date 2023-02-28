namespace Ecommerce.DTOs;

using Ecommerce.Models;
using Ecommerce.Common;
using System.ComponentModel.DataAnnotations;

public class UserDTO : BaseDTO<User>
{
    [MinLength(3, ErrorMessage = "Given name is have less than 3 characters!")]    
    public string? Name {get; set;}

    [EmailAddress(ErrorMessage = "Given email is not valid!")]
    public string? Email {get; set;}
    
    [Password]
    public string? Password {get; set;}    
    public string? Avatar {get; set;}

    public override void UpdateModel(User model)
    {
        model.Name = Name ?? model.Name;
        model.Email = Email ?? model.Email;
        model.Password = Password ?? model.Password;
        model.Avatar = Avatar ?? "";
        model.UpdateAt = DateTime.Now;      
    }

    public override bool CreateModel(User model)
    {
        if(Name == null || Email == null || Password == null)
            return false;
        
        UpdateModel(model);
        return true;
    }
}