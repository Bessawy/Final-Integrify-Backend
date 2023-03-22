namespace Ecommerce.Common;

using System.ComponentModel.DataAnnotations;

// This Attribute is created to validate Password properties or field.
// Password length must be more than 5 and less than 10.
// Password must contain atleast 1 upper character and atleat 1 lower character.
// Password must contain atlaest one special character.
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        string reason = "";
        string Password;

        // If password is set to null, bypass the validation!
        if(value == null)
            return ValidationResult.Success;
        
        Password = (string)value;

        // Password between 5 to 10 characters.
        if (Password.Length < 5 || Password.Length > 10) 
            reason = "Your new password must be at least 5, and no more than 10 characters. ";
        
        // Password must contain atleast 1 upper character.
        if (!Password.Any(char.IsUpper))
            reason += "Password must have atleat 1 upper character. ";

        //  Password must contain atleast 1 lower character.
        if (!Password.Any(char.IsLower))
            reason += "Password must have atleat 1 lower character. ";

        // Password must contain atlaest one special character.
        if (Password.Any(ch => !char.IsLetterOrDigit(ch)))
            reason +=  "Your new password must contain at least 1 symbol character.";
        
        if (reason == "") 
            return ValidationResult.Success;
        else
            return new ValidationResult(reason);
    }
    
}