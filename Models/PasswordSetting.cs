namespace Ecommerce.Models;

public class PasswordSetting
{
    public int MinLength {get; set;}
    public int MaxLength {get; set;}
    public string SpecialChar {get; set;} = null!;
}