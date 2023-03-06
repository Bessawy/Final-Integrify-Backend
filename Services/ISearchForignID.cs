namespace Ecommerce.Services;

public interface ISearchForignID
{
    Task<bool> IsForignIDValidAsync(int? id);
}