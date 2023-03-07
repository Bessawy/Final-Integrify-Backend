namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleService(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityRole<int>?> GetbyName(string role)
    {
        return await _roleManager.FindByNameAsync(role);
    }

    public async Task<IdentityRole<int>> CreateRole(string role)
    {
        var roleObj = new IdentityRole<int>{ Name = role};
        var result = await _roleManager.CreateAsync(roleObj);
        return roleObj;
    }

    public async Task<IdentityRole<int>> FindRoleElseCreate(string role)
    {
        var roleObj = await GetbyName(role);
        if(roleObj is not null)
            return roleObj;
        return await CreateRole(role);
    }
}