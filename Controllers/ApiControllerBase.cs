namespace Ecommerce.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

[ApiController]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("api/v1/[controller]s")]
public class ApiControllerBase : ControllerBase
{   
    // Get user id from the Bearer Token
    public string? GetUserIdFromToken()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity != null)
        {
            var identifer = identity.FindFirst(ClaimTypes.NameIdentifier);
            if(identifer is not null)
                return identifer.Value;
        }
        return null;
    }
}
