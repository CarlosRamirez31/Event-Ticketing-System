using EventTicketing.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EventTicketing.Application.Infrastructure.Persistence
{
    public class EventTicketingContext : DbContext
    {
        public EventTicketingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventPrice> EventPrices { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Zone> Zones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
