using EventTicketing.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketing.Application.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.EventId);

            builder.Property(x => x.Name).HasMaxLength(90);
            builder.Property(x => x.Location).HasMaxLength(255);
            builder.Property(x => x.EventCreatedBy).HasMaxLength(90);
        }
    }
}
