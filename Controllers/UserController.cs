namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

public class UserContoller : ApiControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UserContoller> _logger;

    UserContoller(IUserService service, ILogger<UserContoller> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpRequestDTO request)
    {
        var user = await _service.SignUpAsync(request);
        if(user is null)
            return BadRequest();
        return Ok(UserSignUpResponseDTO.FromUser(user));
    }
}