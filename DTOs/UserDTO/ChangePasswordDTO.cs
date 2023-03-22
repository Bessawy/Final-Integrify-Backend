namespace Ecommerce.DTOs;

using Ecommerce.Common;

public class ChangePasswordDTO
{
    [Password]
    public string OldPassword {get; set;} = null!;

    [Password]
    public string NewPassword {get; set;} = null!;
}