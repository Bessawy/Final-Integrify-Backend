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
    private readonly ITokenService _tokenService;

    public UserService(UserManager<User> userManager, IRoleService roleService
        , ITokenService tokenService)
    {
        _userManager = userManager;
        _roleService = roleService;
        _tokenService = tokenService;
    }

    public async Task<UserSignInResponseDTO?> SignInAsync(UserSignInRequestDTO request)
    {
        var user = await FindUserByEmailAsync(request.Email);
        if(user is null)
            return null;
        if(!await _userManager.CheckPasswordAsync(user, request.Password))
            return null;
        var role = await _userManager.GetRolesAsync(user);
        user.Role = role[0];
        return _tokenService.GenerateToken(user);
    }

    public async Task<User?> FindUserByEmailAsync(string email)
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

    public async Task<bool> DeleteAsync(string email)
    {   
        var user = await FindUserByEmailAsync(email);
        if(user == null)
            return false;
        var result = await _userManager.DeleteAsync(user);
        if(result.Succeeded)
            return true;
        return false;
    }


}