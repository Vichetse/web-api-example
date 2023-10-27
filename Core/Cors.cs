namespace WebApi.Core;

internal static class CorsExtension
{
    public static void AddMyCors(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                    policy.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
            );
        });
    }
}