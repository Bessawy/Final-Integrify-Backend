using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Identity;
using Ecommerce.DTOs;
using Google.Apis.Auth;

namespace Ecommerce.Controllers;

[Route("api/v1/google")]
public class GoogleController : ApiControllerBase
{   
    private readonly ILogger<GoogleController> _logger;
    private readonly SignInManager<User> _signInManage;
    private readonly IUserService _service;
    
    public GoogleController(SignInManager<User> signInManager, 
        ILogger<GoogleController> logger, IUserService service)
    {
        _logger = logger;
        _signInManage = signInManager;
        _service = service;
    }  

    // Allow user to login in with his/her google credentials.
    // If user email does not exist, create an accout and return the token,
    // otherwise, simply return the token!
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserSignInResponseDTO>> GoogleResponse(GoogleDTO request)
    {
        // varify given user credentials by sending the received token to the google server!
        var payload = GoogleJsonWebSignature
         .ValidateAsync(request.Credential, new GoogleJsonWebSignature.ValidationSettings()).Result;

        if (payload is null)
            return Unauthorized();

        // return the access_token for the given user payload.
        return await _service.GoogleLogInAsync(payload);;
    }
}