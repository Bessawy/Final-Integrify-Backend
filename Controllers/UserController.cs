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
        var user = await GetUserFromToken();
        if(user is not null)
            return Ok(UserDTO.FromUser(user!));
        return BadRequest();
    }

    [HttpPut("profile/info")]
    public async Task<ActionResult<UserDTO?>> UpdateCurrentUserInfo(UserDTO request)
    {
        var user = await GetUserFromToken();
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
        var user = await GetUserFromToken();
        if(user is null)
            return false;

        var updateUser = await _service.UpdatePasswordAsync(request, user);
        return true;
    }

    [HttpPost("{id:int}/cart")]
    public async Task<ActionResult<CartDTO>> AddToCart(int id)
    {
        var user = await GetUserFromToken();
        if(user is null)
            return BadRequest();

        var carItem = await _service.AddProductToCartAsync(id, user);
        if(carItem is null)
            return BadRequest();

        return new CartDTO
        {
            Product = carItem!.Product,
            Count = carItem.Count
        };
    }

    public async Task<User?> GetUserFromToken()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        
        if(identity != null)
        {
            var id = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _service.FindUserByIdAsync(id);
            return user;
        }
        return null;
    }
}