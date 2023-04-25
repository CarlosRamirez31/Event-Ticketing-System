namespace EventTicketing.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapSwagger(this WebApplication app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(setting =>
            {
                setting.Path = "/swagger";
            });

            return app;
        }
    }
}
