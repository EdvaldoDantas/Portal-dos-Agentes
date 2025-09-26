using Microsoft.AspNetCore.Diagnostics;

namespace Portal_dos_Agentes.Server
{
    public static class GlobalErrorHandling
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is not null)
                    {
                        logger.LogError(exceptionHandlerPathFeature.Error, "Erro não tratado capturado pelo middleware global.");
                    }

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { error = "Ocorreu um erro inesperado. Tente novamente mais tarde." });
                });
            });

            return app;
        }
    }
}
