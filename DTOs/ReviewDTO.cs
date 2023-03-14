namespace Ecommerce.DTOs;

using Ecommerce.Models;

public class ReviewDTO
{
    public string? UserName {get; set;}
    public int ProductId {get; set;}
    public int Rate {get; set;}
    public string Comment {get; set;} = string.Empty;
}