namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface ITokenService
{
    UserAuthResponseDTO GenerateToken(User user);
    JwtTokenSetting GetTokenSetting(string role);
}