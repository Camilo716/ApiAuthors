using System.ComponentModel.DataAnnotations;

namespace ApiAuthors.Validations;
public class CapitalizedWordsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        bool isNull = value == null || String.IsNullOrEmpty(value.ToString());
        if (isNull) 
        {
            return ValidationResult.Success;
        }
        
        string valueStr = value.ToString()!; 

        string[] words = valueStr.Split(' ');

        foreach (var word in words)
        {
            bool wordDoesNotStartWithCap = char.IsLower(word[0]);
            if (wordDoesNotStartWithCap)
            {
                return new ValidationResult(ErrorMessage ?? "Each word must start with an uppercase letter.");
            }
        }

        return ValidationResult.Success;
    }
}
