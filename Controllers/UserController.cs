namespace Ecommerce.Controllers;

using Ecommerce.DTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// Cannot inherit from CrudSurvicee as User Model can't be used as TModel.
// Basic crud operation is not created for userIdentity Model!
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
        
        // Reutrn either empty Badrequest or a list of occured errors if not null!
        if(user is null && result is not null)
            return BadRequest(result.Errors.ToList());
        else if(user is null)
            return BadRequest();

        return Ok(UserDTO.FromUser(user));
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(UserSignInRequestDTO request)
    {
        try
        {
            var response = await _service.SignInAsync(request);
            if(response is null)
                return NotFound("User email not found!");
            else
                return Ok(response);
        }
        catch(UnauthorizedAccessException e)
        {
            return Unauthorized($"Password is not correct: ({e.Message})!");
        }
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
            return Unauthorized();
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return NotFound();

        return Ok(UserDTO.FromUser(user));
    }

    [HttpPost("profile/info")]
    public async Task<ActionResult<UserDTO?>> UpdateCurrentUserInfo(UserDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return NotFound();

        var updateUser = await _service.UpdateUserInfoAsync(request, user);
        if(updateUser is null)
            return BadRequest();
            
        return Ok(UserDTO.FromUser(updateUser));
    }

    [HttpPost("profile/password")]
    public async Task<IActionResult> UpdateCurrentUserPassowrd(ChangePasswordDTO request)
    {
        var userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();
        
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return NotFound();

        var updateUser = await _service.UpdatePasswordAsync(request, user);
        if(updateUser)
            return Ok(new {Message = "Password has changed successfully!"});
        return BadRequest();
    }
}