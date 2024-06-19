namespace Yomikaze.Domain.Identity.Models;

public class CustomDateRangeAttribute() : RangeAttribute(typeof(DateTime), "1900-01-01", "")
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime currentValue)
        {
            return ValidationResult.Success;
        }

        DateTime minimumDate = Minimum as DateTime? ?? new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime currentDate = DateTime.Now;
        string? fieldName = validationContext.MemberName;
        return minimumDate <= currentValue && currentValue <= currentDate
            ? ValidationResult.Success
            : new ValidationResult($"The field {fieldName} must be between {minimumDate} and {currentDate}.");
    }
}