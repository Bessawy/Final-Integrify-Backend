namespace Ecommerce.DTOs;

using System.ComponentModel.DataAnnotations;
using Ecommerce.Models;

public class ProductDTO : BaseDTO<Product>
{
    public string? Title {get; set;} = null!;

    [Range(0, int.MaxValue, ErrorMessage = "The price can't be negative!")]
    public int? Price {get; set;}
    public string? Description {get; set;}
    public int? CategoryId {get; set;}
    public ICollection<string>? Images {get; set;}

    public override bool CreateModel(Product model)
    {
        if(Title is null || Price is null)
            return false;
            
        UpdateModel(model);
        return true;
    }

    public override void UpdateModel(Product model)
    {
        model.Title = Title ?? model.Title;
        model.Price = Price ?? model.Price;
        model.Images = Images ?? model.Images;
        model.Description = Description ?? model.Description;
        model.CategoryId = CategoryId ?? model.CategoryId;
    }
}