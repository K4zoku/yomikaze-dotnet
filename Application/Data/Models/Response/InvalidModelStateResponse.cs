using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Yomikaze.Application.Data.Models.Response;

public static class InvalidModelStateResponse 
{
    public static IActionResult Factory(ActionContext context)
    {
        Dictionary<string, IEnumerable<string>> errors = new();
        foreach ((string? key, ModelStateEntry? value) in context.ModelState)
        {
            IEnumerable<string> errorsToAdd = from error in value.Errors
                select string.IsNullOrEmpty(error.ErrorMessage)
                    ? "The value you entered is invalid"
                    : error.ErrorMessage;
            errors.Add(key, errorsToAdd.ToArray());
        }

        ResponseModel<object, Dictionary<string, IEnumerable<string>>> problems =
            ResponseModel.CreateError("Validation errors", errors);
        return new BadRequestObjectResult(problems);
    }
}