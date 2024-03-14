using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Helpers.Attributes;

public class AllowedContentTypesAttribute(string[] types) : ValidationAttribute("This type of file is not allowed")
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return ValidationResult.Success; // skip validation if property is not a file  
        }

        return Array.Exists(types, type => type.Equals(file.ContentType, StringComparison.InvariantCultureIgnoreCase))
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}