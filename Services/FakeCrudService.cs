namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using System.Collections.Concurrent;

public class CrudService<TModel, TDTo> : ICrudService<TModel, TDTo>
    where TModel : BaseModel, new()
    where TDTo : BaseDTO<BaseModel>
{
    private ConcurrentDictionary<int, TModel> _items = new();
    private int _itemId;
    public TModel? Create(TDTo request)
    {
        if(request == null)
            return null;

        var item = new TModel
        {
            Id = Interlocked.Increment(ref _itemId)
        };

        request.UpdateModel(item);
        return item;
    }

    public bool Delete(int id)
    {
        if(_items.ContainsKey(id))
            return _items.Remove(id, out var _ );
        return false;
    }

    public TModel? Get(int id)
    {
        if(_items.ContainsKey(id))
            return _items[id];
        return null;
    }

    public ICollection<TModel> GetAll()
    {
        return _items.Values.ToArray();
    }

    public TModel? Update(int id, TDTo request)
    {
        if(!_items.ContainsKey(id))
            return null;
            
        request.UpdateModel(_items[id]);
        return _items[id];
    }
}

