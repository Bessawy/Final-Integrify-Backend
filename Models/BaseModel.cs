namespace Ecommerce.Models;

public abstract class BaseModel
{
    public int Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdateAt {get; set;}
}