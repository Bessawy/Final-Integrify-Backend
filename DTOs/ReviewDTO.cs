namespace Ecommerce.DTOs;

using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

public class ReviewDTO
{
    public string? UserName {get; set;}
    public int ProductId {get; set;}

    [Range(0.0, 5.0, ErrorMessage = "Rate must be between 0 and 5 inclusive")]
    public int Rate {get; set;}
    public string Comment {get; set;} = string.Empty;

    public static ReviewDTO FromReview(Review review)
    {
        return new ReviewDTO
        {
            UserName = review.User.Name,
            Rate = review.Rate,
            Comment = review.Comment,
            ProductId = review.ProductId,
        };
    }
}