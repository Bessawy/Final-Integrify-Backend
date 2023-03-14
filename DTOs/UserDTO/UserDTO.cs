namespace Ecommerce.DTOs;

using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    public int? Id {get; set;}
    public string? Name {get; set;}
    
    [EmailAddress]
    public string? Email {get; set;}
    public string? Role {get; set;}
    public string? Avatar {get; set;}

    public static UserDTO FromUser(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Avatar = user.Avatar
        };
    }

    public void Update(User user)
    {
        user.Name = Name ?? user.Name;
        user.Email = Email ?? user.Email;
        user.Role = Role ?? user.Role;
        user.UserName = Email ?? user.Email;
        user.Avatar = Avatar ?? user.Avatar;
    }
}

