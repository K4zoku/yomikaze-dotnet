using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
namespace Yomikaze.Domain.Models;

public class CustomDateRangeAttribute : RangeAttribute
{
    public CustomDateRangeAttribute() : base(typeof(DateTime), "1900-01-01", "") { }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not DateTime currentValue) return ValidationResult.Success;
      
        DateTime currentDate = DateTime.Now;

        if (currentValue <= currentDate)
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("Birthday cannot be greater than current date.");
    }
}
