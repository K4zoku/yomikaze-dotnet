﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Yomikaze.Domain.Attributes;

public class AllowedContentTypesAttribute(string[] types) : ValidationAttribute("This type of file is not allowed")
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        PropertyInfo? property = value?.GetType().GetProperty("ContentType");
        string? contentType = property?.GetValue(value)?.ToString();

        return property is null || Array.Exists(types,
            type => type.Equals(contentType, StringComparison.InvariantCultureIgnoreCase))
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}