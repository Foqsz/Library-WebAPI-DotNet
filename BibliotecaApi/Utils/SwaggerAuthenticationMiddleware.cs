using System.Net;
using System.Net.Http.Headers;
using System.Text;
namespace ApiSwagger.SwaggerUtils;
public class SwaggerAuthenticationMiddleware
{
    private readonly RequestDelegate next;

    public SwaggerAuthenticationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Pega as credenciais a partir do header do request
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header.Parameter);

                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');

                var username = credentials[0];
                var password = credentials[1];

                //valida as credenciais
                if (username.Equals("macoratti") && password.Equals("numsey#123"))
                {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }

            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}