namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;

public class DbUserService : DbCrudService<User, UserDTO>
{
    public DbUserService(AppDbContext context) : base(context)
    {
    }
}