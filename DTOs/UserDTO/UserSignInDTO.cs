namespace Ecommerce.DTOs;

using System.ComponentModel.DataAnnotations;

public class UserSignInRequestDTO
{
    [EmailAddress]
    public string Email {get; set;} = null!;
    
    public string Password {get; set;} = null!;
}

public class UserSignInResponseDTO
{
    public string Token {get; set;} = null!;
    public DateTime ExpireTime {get; set;}
}

public class GoogleDTO
{
    public string Credential {get; set;} = null!;
}
