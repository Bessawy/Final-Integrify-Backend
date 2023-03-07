namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;

public interface IUserService
{
    Task<User?> SignUpAsync(UserSignUpRequestDTO request);
    Task<User?> SignInAsync(UserSignInRequestDTO request);

}