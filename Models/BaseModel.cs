namespace Ecommerce.Models;

public abstract class BaseModel
{
    public int Id {get; set;}
    public DateTime CreatedAt {get;} = DateTime.Now;
    public DateTime UpdateAt {get; set;} = DateTime.Now;
}