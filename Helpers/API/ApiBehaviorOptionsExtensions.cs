using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Helpers.API;

public static class ApiBehaviorOptionsExtensions
{
    public static IMvcBuilder ConfigureApiBehaviorOptionsYomikaze(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                Dictionary<string, IEnumerable<string>> errors = new();
                foreach ((string? key, ModelStateEntry? value) in context.ModelState)
                {
                    IEnumerable<string> errorsToAdd = value.Errors
                        .Where(error => !string.IsNullOrEmpty(error.ErrorMessage))
                        .Select(error => error.ErrorMessage);
                    errors.Add(key, errorsToAdd);
                }

                ResponseModel<object, Dictionary<string, IEnumerable<string>>> problems =
                    ResponseModel.CreateError("Validation errors", errors);
                return new BadRequestObjectResult(problems);
            };
        });

        return builder;
    }
}