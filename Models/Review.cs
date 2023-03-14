namespace Ecommerce.Models;

using System.Text.Json.Serialization;

public class Review
{
    [JsonIgnore]
    public User User {get; set;} = null!;
    public int UserId {get; set;}

    [JsonIgnore]
    public Product Product {get; set;} = null!;
    public int ProductId {get; set;}
    public int Rate {get; set;}
    public string Comment {get; set;} = string.Empty;
}