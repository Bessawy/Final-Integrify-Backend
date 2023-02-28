namespace Ecommerce.Common;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        bool validPassword = false;
        string reason = "", Password;

        if(value == null)
            return ValidationResult.Success;
        
        Password = value.ToString();

        if (String.IsNullOrEmpty(Password) || Password.Length < 8) 
            reason = "Your new password must be at least 8 characters long. ";
        
        Regex reSymbol = new Regex("[^a-zA-Z0-9]");
        if (!reSymbol.IsMatch(Password)) 
            reason +=  "Your new password must contain at least 1 symbol character.";
        else
            validPassword = true;
        
        if (validPassword) 
            return ValidationResult.Success;
        else
            return new ValidationResult(reason);
    }
    
}