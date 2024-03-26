using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public class CustomDateRangeAttribute() : RangeAttribute(typeof(DateTime), "1900-01-01", "")
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime currentValue)
        {
            return ValidationResult.Success;
        }

        DateTime currentDate = DateTime.Now;

        return currentValue <= currentDate ? ValidationResult.Success : new ValidationResult("Birthday cannot be greater than current date.");
    }
}