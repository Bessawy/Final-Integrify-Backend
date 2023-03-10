namespace Ecommerce.DTOs;

public class UserAuthResponseDTO
{
    public string Token {get; set;} = null!;
    public DateTime ExpireTime {get; set;}

}