namespace Ecommerce.DTOs;

using Ecommerce.Common;
using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    public int? Id {get; set;}
    public string Name {get; set;} = null!;
    [EmailAddress]
    public string Email {get; set;} = null!;
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
}