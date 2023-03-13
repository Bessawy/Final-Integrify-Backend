namespace Ecommerce.Services;

using Microsoft.AspNetCore.Identity;

public interface IRoleService
{
    Task<IdentityRole<int>?> GetbyName(string role);
    Task<IdentityRole<int>> CreateRole(string role);
    Task<IdentityRole<int>> FindRoleElseCreate(string role);
}