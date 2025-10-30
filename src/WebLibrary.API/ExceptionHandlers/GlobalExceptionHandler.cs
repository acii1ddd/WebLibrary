using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.BLL.Exceptions;

namespace WebLibrary.API.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, 
        Exception e, 
        CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exMessage}, time of occurrence {exTime}",
            e.Message, DateTime.UtcNow);
        
        var (statusCode, errorMessage) = e switch
        {
            OperationCanceledException operationCanceledEx => (HttpStatusCode.BadRequest, operationCanceledEx.Message),
            BadRequestException badRequestEx => (HttpStatusCode.BadRequest, badRequestEx.Message),
            NotFoundException notFoundEx => (HttpStatusCode.NotFound, notFoundEx.Message),
            ValidationException validationEx => (HttpStatusCode.BadRequest, validationEx.Message),
            _ => (HttpStatusCode.InternalServerError, e.Message)
        };
            
        var problemDetails = new ProblemDetails
        {
            Detail = errorMessage,
            Type = e.GetType().Name,
            Status = (int) statusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        
        if (e is ValidationException ve)
        {
            problemDetails.Extensions.Add("validationErrors", ve.Message);
        }
                
        context.Response.StatusCode = (int) statusCode;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}