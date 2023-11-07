using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Application.Data.Attributes;

public class AllowedContentTypesAttribute : ValidationAttribute
{
    private readonly string[] _types;
    private readonly StringComparison _stringComparison = StringComparison.InvariantCultureIgnoreCase;


    public AllowedContentTypesAttribute(string[] types)
    {
        _types = types;
    }

    public AllowedContentTypesAttribute(string[] types, StringComparison stringComparison)
    {
        _types = types;
        _stringComparison = stringComparison;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file && !Array.Exists(_types, type => type.Contains(file.ContentType, _stringComparison)))
        {
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return $"This content type is not allowed!";
    }
}
