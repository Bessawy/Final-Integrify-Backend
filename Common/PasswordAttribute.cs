namespace Ecommerce.Common;

using Ecommerce.Models;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class PasswordAttribute : ValidationAttribute
{
    private readonly IOptions<PasswordSetting> _setting;
    public override bool IsValid(object? value)
    {
        // Password not null
        if(value == null)
            return false;

        // Password length greater than minLength,and less than maxlength.
        // Password contains atleast 1 upper and 1 lower character,
        // and no white space.
        var passwd = (string)value;
        if (passwd.Length >  _setting.Value.MinLength 
            || passwd.Length < _setting.Value.MaxLength 
            || !passwd.Any(char.IsUpper) 
            || !passwd.Any(char.IsLower)
            || passwd.Contains(" "))
            return false;

        // Password contains atleast 1 special character.
        char[] specialChArray = _setting.Value.SpecialChar.ToCharArray();
        foreach (char ch in specialChArray) {
            if (passwd.Contains(ch))
                return true;
            }

        return false;
    }
}