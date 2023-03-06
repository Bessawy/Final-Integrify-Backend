namespace Ecommerce.DTOs;

using Ecommerce.Models;

public class CategorytDTO : BaseDTO<Category>
{
    public string Name {get; set;} = null!;
    public ICollection<string> Images {get; set;} = null!;   

    public override bool CreateModel(Category model)
    {
        UpdateModel(model);
        return true;
    }

    public override void UpdateModel(Category model)
    {
        model.Name = Name;
        model.Images = new List<string>(Images);
    }
}