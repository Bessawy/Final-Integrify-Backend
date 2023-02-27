namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;

public class UserController : CrudController<User, UserDTO>
{
    private readonly ILogger<UserController> _logger;
    public UserController(ICrudService<User, UserDTO> service,
        ILogger<UserController> logger) : base(service)
    {
        _logger = logger;
    }
}