using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yomikaze.Application.Helpers;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not HttpResponseException httpResponseException)
        {
            return;
        }

        context.Result = new ObjectResult(httpResponseException.Value)
        {
            StatusCode = httpResponseException.StatusCode
        };

        context.ExceptionHandled = true;
    }

    public int Order => int.MaxValue - 10;
}