namespace Ecommerce.DTOs;

using System.Text.Json.Serialization;
using Ecommerce.Models;

public class ProductDTO : BaseDTO<Product>
{
    public string Title {get; set;} = null!;
    public int Price {get; set;}
    public string? Description {get; set;}
    public CategorytDTO? Category {get; set;} = null!;
    public ICollection<string> Images {get; set;} = null!; 

    public override bool CreateModel(Product model)
    {
        UpdateModel(model);
        return true;
    }

    public override void UpdateModel(Product model)
    {
        model.Title = Title;
        model.Price = Price;
        model.Description = Description;
        Category? itemCategory = new();

        if(Category != null)
            Category.CreateModel(itemCategory);
        else
            itemCategory = null;
        
        model.Category = itemCategory;
        model.Images = new List<string>(Images);
    }
}