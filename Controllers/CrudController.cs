namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public abstract class CrudController<TModel, TDTo> : ApiControllerBase
{
    protected readonly ICrudService<TModel, TDTo> _service;

    public CrudController(ICrudService<TModel, TDTo> service) 
    {  
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<ICollection<TModel>> GetAll()
    {
        return await _service.GetAllAsync();
    } 
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TModel?>> Get(int id)
    {
        var item = await _service.GetAsync(id);
        if(item == null)
            return NotFound("Item not found!");
        return Ok(item);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TModel?>> Create(TDTo request)
    {
        var item = await _service.CreateAsync(request);
        if(item == null)
            return BadRequest();
        return Ok(item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if(await _service.DeleteAsync(id))
            return Ok(new {Message = "Item is deleted"});
        else    
            return NotFound("Item not found!");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TModel?>> Update(int id, TDTo request)
    {
        var item = await _service.UpdateAsync(id, request);
        if(item == null)
            return NotFound("Item not found!");
        else 
            return Ok(item);
    }

}