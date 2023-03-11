namespace Ecommerce.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    [MaxLength(256)]
    public string Name {get; set;} = null!;
    public string? Avatar {get; set;}
    public string Role {get; set;} = "customer";

    [JsonIgnore]
    public ICollection<CartItem> Carts {get; set;} = null!;

    
}