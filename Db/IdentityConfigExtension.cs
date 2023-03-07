namespace Ecommerce.Db;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;

public static class IdentityConfigExtension
{
    public static void AddIdentityConfig(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("users");
        builder.Entity<IdentityRole<int>>().ToTable("roles");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims");
        builder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("user_logins");
        builder.Entity<IdentityUserToken<int>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<int>>().ToTable("user_Roles");
    }
}