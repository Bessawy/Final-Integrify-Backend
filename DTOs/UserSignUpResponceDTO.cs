namespace Ecommerce.DTOs;

using Ecommerce.Models;

public class UserSignUpResponseDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string Role {get; set;} = null!;
    public string? Avatar {get; set;} = null!;

    public static UserSignUpResponseDTO FromUser(User user)
    {
        return new UserSignUpResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Avatar = user.Avatar
        };
    }

}