using SharedUtilities.CustomExceptions;
using System.Net;

namespace SharedUtilities.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await SendResponse(ex, context);
            }
        }

        private async Task SendResponse(Exception ex, HttpContext context)
        {
            switch(ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound; 
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case InternalServerErrorExceptiopn:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    break;

            }
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
