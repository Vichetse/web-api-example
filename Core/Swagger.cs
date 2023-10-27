using NSwag;
using NSwag.Generation.Processors.Security;

namespace WebApi.Core;

internal static class SwaggerExtension
{
    public static void AddMySwagger(this IServiceCollection services)
    {
        services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("bearer", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}"
                    });
                document.GenerateEnumMappingDescription = true;
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            }
        );
    }
}