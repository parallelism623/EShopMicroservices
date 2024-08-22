using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;


namespace BuildingBlocks.Middlewares;
public class GlobalHandlingExceptionMiddleware(ILogger<GlobalHandlingExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            context = HandlingException(ex, context);
            
        }
    }

    private HttpContext HandlingException(Exception exception, HttpContext context)
    {
        var response = new
        {
            StatusCodes = GetStatusCode(exception),
            Message = exception.Message
        };
        context.Response.StatusCode = GetStatusCode(exception);
        context.Response.ContentType = "application/json";
        context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((response))));
        return context;
    }
    private int GetStatusCode(Exception exception)
        => exception switch
        {
            BadRequestException => 400,
            ValidationErrorException => 400,
            NotFoundException => 404, 
            InternalServerException => 500, 
            _ => 500
        };
    private string GetMessageResponse(Exception exception)
        => exception switch
        {
            BadRequestException => "Bad Request",
            ValidationErrorException => "Bad Request",
            NotFoundException => "Not Found",
            InternalServerException => "Internal Server Error",
            _ => "Internal Server Error"
        };

}
