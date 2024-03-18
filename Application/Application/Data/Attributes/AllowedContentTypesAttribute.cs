using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Application.Data.Attributes;

public class AllowedContentTypesAttribute(string[] types) : ValidationAttribute("This type of file is not allowed")
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return ValidationResult.Success;
        }

        string contentType = file.ContentType;
        return Array.Exists(types,
            type => type.Equals(contentType, StringComparison.InvariantCultureIgnoreCase))
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}