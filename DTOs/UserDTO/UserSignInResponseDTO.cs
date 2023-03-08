namespace Ecommerce.DTOs;

public class UserSignInResponseDTO
{
    public string Token {get; set;} = null!;
    public DateTime ExpireTime {get; set;}

}