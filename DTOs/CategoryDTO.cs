namespace Ecommerce.DTOs;

using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

public class CategoryDTO : BaseDTO<Category>
{
    [MaxLength(64)]
    public string? Name {get; set;}
    public string? Image {get; set;} 

    public override bool CreateModel(Category model)
    {
        if(Name is null)
            return false;

        UpdateModel(model);
        return true;
    }

    public override void UpdateModel(Category model)
    {
        model.Name = Name ?? model.Name;
        model.Image = Image ?? model.Image;
    }
}