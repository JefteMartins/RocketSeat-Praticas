using CashFlow.Communication.Response;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }

    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException errorOnValidationException)
        {
            var erroResponse = new ResponseErrorJson(errorOnValidationException.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(erroResponse);
        }
        else if (context.Exception is NotFoundException notFoundException)
        {

            var erroResponse = new ResponseErrorJson(notFoundException.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundObjectResult(erroResponse);
        }
        else
        {
            var errorResponse = new ResponseErrorJson(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var erroResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(erroResponse);
    }
}
