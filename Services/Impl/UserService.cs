namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<User?> SignInAsync(UserSignInRequestDTO request)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> SignUpAsync(UserSignUpRequestDTO request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        
        if(result.Succeeded)
            return user;
        else
            return null;
    }
}