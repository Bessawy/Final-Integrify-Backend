namespace Ecommerce.Models;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

public class Product : BaseModel
{
    public string Title {get; set;} = null!;
    public int Price {get; set;}
    public string? Description {get; set;}
    public Category? Category {get; set;} = null!;

    [JsonIgnore]
    public int? CategoryId {get; set;}
    
    [Column(TypeName = "jsonb")]
    public ICollection<string> Images {get; set;} = null!; 
}