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
    [QueryParam("status")]
    public async Task<ActionResult<User?>> GetByStatus([FromQuery] User.UserRole status)
    {
        Console.WriteLine(status);
        var item = await _service.GetAllAsync();
        if(item == null)
            return NotFound("Item not found!");
        return Ok(item);
    }

}