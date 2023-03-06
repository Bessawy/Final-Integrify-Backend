namespace Ecommerce.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Category : BaseModel
{
    [MaxLength(64)]
    public string Name {get; set;} = null!;
    public string Image {get; set;} = null!; 
    
    [JsonIgnore]
    public ICollection<Product> Products {get; set;} = null!; 
}