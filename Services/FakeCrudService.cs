namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using System.Collections.Concurrent;

public class FakeCrudService<TModel, TDTo> : ICrudService<TModel, TDTo>
    where TModel : BaseModel, new()
    where TDTo : BaseDTO<TModel>
{
    private ConcurrentDictionary<int, TModel> _items = new();
    private int _itemId;

    //TODO : email is unquie (Bad request if email is used)
    public Task<TModel?> CreateAsync(TDTo request)
    {
        var item = new TModel
        {
            Id = Interlocked.Increment(ref _itemId)
        };

        if(request.CreateModel(item))
        {
            _items[item.Id] = item;
            return Task.FromResult<TModel?>(item);
        }
        return Task.FromResult<TModel?>(null);
    }

    public Task<bool> DeleteAsync(int id)
    {
        if(_items.ContainsKey(id))
            return Task.FromResult(_items.Remove(id, out var _ ));
        return Task.FromResult(false);
    }

    public Task<TModel?> GetAsync(int id)
    {
        if(_items.ContainsKey(id))
            return Task.FromResult<TModel?>(_items[id]);
        return Task.FromResult<TModel?>(null);
    }

    public Task<ICollection<TModel>> GetAllAsync()
    {
        return Task.FromResult<ICollection<TModel>>(_items.Values);
    }

    public Task<TModel?> UpdateAsync(int id, TDTo request)
    {
        if(!_items.ContainsKey(id))
            return Task.FromResult<TModel?>(null);

        request.UpdateModel(_items[id]);
        return  Task.FromResult<TModel?>(_items[id]);
    }

    public Task<ICollection<TModel>> GetAllAsync(int offset, int limit)
    {
        throw new NotImplementedException();
    }
}

