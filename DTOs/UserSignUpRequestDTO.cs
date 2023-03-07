namespace Ecommerce.DTOs;
using Ecommerce.Common;

using System.ComponentModel.DataAnnotations;

public class UserSignUpRequestDTO
{
    public string Name {get; set;}  = null!;

    [EmailAddress]
    public string Email {get; set;} = null!;

    [Password]
    public string Password {get; set;} = null!;
}