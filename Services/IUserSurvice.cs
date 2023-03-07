namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<(User?, IdentityResult?)> SignUpAsync(UserSignUpRequestDTO request);
    Task<User?> SignInAsync(UserSignInRequestDTO request);
    Task<User?> FindUserByEmail(string email);

}