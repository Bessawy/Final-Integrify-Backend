namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<(User?, IdentityResult?)> SignUpAsync(UserSignUpRequestDTO request);
    Task<UserSignInResponseDTO?> SignInAsync(UserSignInRequestDTO request);
    Task<User?> FindUserByEmail(string email);

}