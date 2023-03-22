namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config) =>
        _config = config;

    public UserSignInResponseDTO GenerateToken(User user)
    {

        // Setting up the payload
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // Get Jwt setting based on user role
        JwtTokenSetting config = GetTokenSetting(user.Role);

        var signInSecret = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
            SecurityAlgorithms.HmacSha256  
        );

        var expiration = DateTime.Now.AddHours(config.Hours);
        var token = new JwtSecurityToken(config.Issuer, 
            config.Auth,
            claims, 
            expires: expiration, 
            signingCredentials: signInSecret);
        var tokenWriter = new JwtSecurityTokenHandler();

        return new UserSignInResponseDTO
        {
            Token = tokenWriter.WriteToken(token),
            ExpireTime = expiration
        };
    }

    public JwtTokenSetting GetTokenSetting(string role)
    {   
        return new JwtTokenSetting
        {
            Secret = _config[$"Jwt_{role}:Secret"],
            Hours = double.Parse(_config[$"Jwt_{role}:ExpirationHours"]),
            Issuer = _config[$"Jwt_{role}:Issuer"],
            Auth = _config[$"Jwt_{role}:Aud"]
        };
    }
}