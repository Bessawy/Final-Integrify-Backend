namespace Ecommerce.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class Category : BaseModel
{
    public string Name {get; set;} = null!;

    [Column(TypeName = "jsonb")]
    public ICollection<string> Images {get; set;} = null!;  
}