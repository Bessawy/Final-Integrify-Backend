namespace Ecommerce.DTOs;

using System.ComponentModel.DataAnnotations;

public class EmailDTO
{
    [EmailAddress]
    public string Email {get; set;} = null!;
}