namespace Ecommerce.DTOs;

using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

public class ReviewDTO
{
    public string? UserName {get; set;}
    public int ProductId {get; set;}

    [Range(0.0, 5.0, ErrorMessage = "Rate must be between 0 and 5")]
    public int Rate {get; set;}
    public string Comment {get; set;} = string.Empty;
}