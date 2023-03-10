namespace Ecommerce.Controllers;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public class UserController : ApiControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService service, ILogger<UserController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpRequestDTO request)
    {
        (var user, var result) = await _service.SignUpAsync(request);
        if(user is null && result is not null)
            return BadRequest(result.Errors.ToList());
        return Ok(UserSignUpResponseDTO.FromUser(user!));
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(UserSignInRequestDTO request)
    {
        var response = await _service.SignInAsync(request);
        if(response is null)
            return Unauthorized();
        else
            return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("is-avaiable")]
    public async Task<bool> CheckEmail(EmailDTO request)
    {
        var user = await _service.FindUserByEmailAsync(request.Email);
        if(user == null)
            return false;
        return true;
    }

   [HttpGet]
   public async Task<ActionResult<UserDTO?>> GetCurrentUser()
   {
    var identity = HttpContext.User.Identity as ClaimsIdentity;
    
    if(identity != null)
    {
        var email = identity.FindFirst(ClaimTypes.Email)!.Value;
        var user = await _service.FindUserByEmailAsync(email);
        return Ok(UserDTO.FromUser(user!));
    }
    return BadRequest();
   }
}