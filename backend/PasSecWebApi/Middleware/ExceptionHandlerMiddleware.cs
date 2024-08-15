using System.Net;
using System.Text.Json;

namespace PasSecWebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next) { _next = next; }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var res = string.Empty;

            switch(ex)
            {
                // add custom error code here. serialize custom response.
                case Exception:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            
            context.Response.StatusCode = (int)statusCode;
            if(res == string.Empty)
            {
                res = JsonSerializer.Serialize(new { Message = "OPPS. Something Went Wrong." });
            }

            return context.Response.WriteAsync(res);
        }

    }
}
