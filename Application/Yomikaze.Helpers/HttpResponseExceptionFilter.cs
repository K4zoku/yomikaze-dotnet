using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Yomikaze.Application.Helpers;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            return;
        }

        if (context.Exception is not HttpResponseException httpResponseException)
        {
            context.Result = new ObjectResult(new { context.Exception.Message })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(context.Exception.Message);
            Console.Error.WriteLine(context.Exception.StackTrace);
            Console.ResetColor();
            context.ExceptionHandled = true;
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