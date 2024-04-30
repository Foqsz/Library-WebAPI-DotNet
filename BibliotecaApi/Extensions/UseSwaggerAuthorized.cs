
namespace ApiSwagger.SwaggerUtils;

public static class SwaggerAuthorization
{
    public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerAuthenticationMiddleware>();
    }
}