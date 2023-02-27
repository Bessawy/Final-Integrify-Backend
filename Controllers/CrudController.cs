namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public abstract class CrudController<TModel, TDTo> : ApiControllerBase
{
    private readonly ICrudService<TModel, TDTo> _service;

    public CrudController(ICrudService<TModel, TDTo> service) 
    {  
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public ICollection<TModel> GetAll()
    {
        return _service.GetAll();
    } 
    
    [HttpGet("{id:int}")]
    public ActionResult<TModel?> Get(int id)
    {
        var item = _service.Get(id);
        if(item == null)
            return NotFound("Item not found!");
        return Ok(item);
    }

    [HttpPost]
    public virtual ActionResult<TModel?> Create(TDTo request)
    {
        var item = _service.Create(request);
        if(item == null)
            return BadRequest();
        return Ok(item);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if(_service.Delete(id))
            return Ok(new {Message = "Item is deleted"});
        else    
            return NotFound("Item not found!");
    }

    [HttpPut("{id:int}")]
    public ActionResult<TModel?> Update(int id, TDTo request)
    {
        var item = _service.Update(id, request);
        if(item == null)
            return NotFound("Item not found!");
        else 
            return Ok(item);
    }

}