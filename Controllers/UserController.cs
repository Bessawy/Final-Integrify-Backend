namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Common;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public class UserController : CrudController<User, UserDTO>
{
    private readonly ILogger<UserController> _logger;
    public UserController(ICrudService<User, UserDTO> service,
        ILogger<UserController> logger) : base(service)
    {
        _logger = logger;
    }

    // ToDo - replace testcase with a valid implementation
    [HttpGet]
    [QueryParam("offset", "limit")]
    public async Task<ActionResult<User?>> GetByStatus([FromQuery] int offset,
        [FromQuery] int limit)
    {

        var item = await _service.GetAllAsync(offset, limit);
        if(item == null)
            return NotFound("Item not found!");
        return Ok(item);
    }

}