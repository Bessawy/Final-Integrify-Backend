namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public class UserController : ApiControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService service, ILogger<UserController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpRequestDTO request)
    {
        (var user, var result) = await _service.SignUpAsync(request);
        if(user is null && result is not null)
            return BadRequest(result.Errors.ToList());
        return Ok(UserSignUpResponseDTO.FromUser(user!));
    }
}