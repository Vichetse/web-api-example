using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Core;

internal static class ErrorException
{
    public static void AddError(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(contextFeature.Error.Message);
                }
            });
        });
    }
}