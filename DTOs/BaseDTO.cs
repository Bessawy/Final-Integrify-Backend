namespace Ecommerce.DTOs;

using Ecommerce.Models;

public abstract class BaseDTO<TModel> where TModel : BaseModel
{
    public abstract void UpdateModel(TModel model); 
    public abstract bool CreateModel(TModel model);   
}