namespace Ecommerce.DTOs;

using System.ComponentModel.DataAnnotations;

public class EmailRequestDTO
{
    [EmailAddress]
    public string Eamil {get; set;} = null!;
}
