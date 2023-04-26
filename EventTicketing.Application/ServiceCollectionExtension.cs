using EventTicketing.Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventTicketing.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventTicketingContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("EventTicketing"))
                );

            return services;
        }
    }
}
