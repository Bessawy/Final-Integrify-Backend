namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            new Claim(JwtRegisteredClaimNames.Aud, _config["Jwt:Aud"]),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
        };

        // Secret
        var secret = _config["Jwt:Secret"];
        var signInSecret = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            SecurityAlgorithms.HmacSha256  
        );

        double hours = double.Parse(_config["Jwt:ExpirationHours"]);
        var expiration = DateTime.Now.AddHours(hours);
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], 
            _config["Jwt:Aud"],
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
}