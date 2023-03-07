namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IRoleService _roleService;

    public UserService(UserManager<User> userManager, IRoleService roleService)
    {
        _userManager = userManager;
        _roleService = roleService;
    }

    public Task<User?> SignInAsync(UserSignInRequestDTO request)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<(User?, IdentityResult?)> SignUpAsync(UserSignUpRequestDTO request)
    {
        var user = new User
        {
            Name = request.Name,
            UserName = request.Email,
            Email = request.Email,
            Role = "customer"
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        var roleObj = await _roleService.FindRoleElseCreate("customer");
        await _userManager.AddToRoleAsync(user, "customer");

        if(!result.Succeeded)
            return (null, result);

        return (user, result);
    }
}