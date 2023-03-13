namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<(User?, IdentityResult?)> SignUpAsync(UserSignUpRequestDTO request);
    Task<UserSignInResponseDTO?> SignInAsync(UserSignInRequestDTO request);
    Task<User?> FindUserByEmailAsync(string email);
    Task<User?> FindUserByIdAsync(string id);
    Task<bool> DeleteAsync(User user);
    Task<User?> UpdateUserInfoAsync(UserDTO request, User user);
    Task<bool> UpdatePasswordAsync(ChangePasswordDTO request, User user);
}
