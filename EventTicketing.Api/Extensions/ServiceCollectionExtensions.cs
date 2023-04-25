namespace EventTicketing.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddOpenApiDocument(opt =>
            {
                opt.Title = "EventTicketing";
                opt.Version = "v1";
            });

            return services;
        }
    }
}
