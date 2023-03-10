namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface ITokenService
{
    UserSignInResponseDTO GenerateToken(User user);
    JwtTokenSetting GetTokenSetting(string role);
}