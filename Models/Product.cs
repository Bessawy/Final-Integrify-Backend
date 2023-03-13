namespace Ecommerce.Models;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Product : BaseModel
{
    [MaxLength(256)]
    public string Title {get; set;} = null!;
    public int Price {get; set;}
    public string? Description {get; set;}
    public Category? Category {get; set;} = null!;
    
    [JsonIgnore]
    public int? CategoryId {get; set;}
    
    [Column(TypeName = "jsonb")]
    public ICollection<string> Images {get; set;} = null!; 

    [JsonIgnore]
    public ICollection<CartItem> Carts {get; set;} = null!;

    [JsonIgnore]
    public ICollection<Review> Reviews {get; set;} = null!;

    public double Rating {get; set;} = 0;
    public int NumberOfReviews {get; set;} = 0;
}