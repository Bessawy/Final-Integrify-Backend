namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;

// Independant class since User model inherits from IdentityUser class
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<User> userManager, IRoleService roleService
        , ITokenService tokenService, AppDbContext dbContext)
    {
        _userManager = userManager;
        _roleService = roleService;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<UserSignInResponseDTO?> SignInAsync(UserSignInRequestDTO request)
    {
        var user = await FindUserByEmailAsync(request.Email);
        if(user is null)
            return null;

        if(!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException();
        
        //-----------------TODO--Remove-Comments------------------
        // var role = await _userManager.GetRolesAsync(user);
        // user.Role = role[0];
        return _tokenService.GenerateToken(user);
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<(User?, IdentityResult?)> SignUpAsync(UserSignUpRequestDTO request)
    {
        string role = "customer";

        var user = new User
        {
            Name = request.Name,
            UserName = request.Email,
            Email = request.Email,
            Role = role,
            Avatar = request.Avater
        };

        IdentityResult result;
        // If password is empty string - create account with no password!
        // No password accounts are mainly used for external logins (ex. Google).
        if(request.Password == "")
            result = await _userManager.CreateAsync(user);
        else
            result = await _userManager.CreateAsync(user, request.Password);

        var roleObj = await _roleService.FindRoleElseCreate(role);
        await _userManager.AddToRoleAsync(user, role);

        if(!result.Succeeded)
            return (null, result);

        return (user, result);
    }

    public async Task<bool> DeleteAsync(User user)
    {   
        var result = await _userManager.DeleteAsync(user);
        if(result.Succeeded)
            return true;
        return false;
    }

    public async Task<User?> UpdateUserInfoAsync(UserDTO request, User user)
    {
        request.Update(user);
        var result = await _userManager.UpdateAsync(user);
        if(result.Succeeded)
            return user;
        return null;
    }

    public async Task<bool> UpdatePasswordAsync(ChangePasswordDTO request, User user)
    {
        var result = await _userManager.ChangePasswordAsync(user, 
            request.OldPassword, request.NewPassword);
        return result.Succeeded;
    }

    public async Task<UserSignInResponseDTO> GoogleLogInAsync(GoogleJsonWebSignature.Payload payload)
    {
         // if google user is not found, add him/her to the database
        var user = await FindUserByEmailAsync(payload.Email);
        if(user == null)
        {   
            var res = await SignUpAsync(new UserSignUpRequestDTO 
            {
                Email = payload.Email,
                Name = payload.Name,
                Password = "",
                Avater = payload.Picture
            });

            user = res.Item1;
        }

        return _tokenService.GenerateToken(user!);
    }
}  