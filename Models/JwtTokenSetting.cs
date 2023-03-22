namespace Ecommerce.Models;

public class JwtTokenSetting
{
    public string Secret{get; set;} = null!;
    public string Issuer{get; set;} = null!;
    public string Auth{get; set;} = null!;
    public double Hours {get; set;}

}