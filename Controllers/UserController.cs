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
        return Ok(UserDTO.FromUser(user!));
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

   [HttpGet("profile")]
   public async Task<ActionResult<UserDTO?>> GetCurrentUser()
   {
         var userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return BadRequest();
            
        if(user is not null)
            return Ok(UserDTO.FromUser(user!));
        return BadRequest();
    }

    [HttpPut("profile/info")]
    public async Task<ActionResult<UserDTO?>> UpdateCurrentUserInfo(UserDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return BadRequest();

        var updateUser = await _service.UpdateUserInfoAsync(request, user);
        if(updateUser is null)
            return BadRequest();

        return Ok(UserDTO.FromUser(updateUser));
    }

    [HttpPut("profile/password")]
    public async Task<bool> UpdateCurrentUserPassowrd(ChangePasswordDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return false;
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return false;

        var updateUser = await _service.UpdatePasswordAsync(request, user);
        return true;
    }
}