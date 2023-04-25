using EventTicketing.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketing.Application.Infrastructure.Persistence.Configurations
{
    public class EventPriceConfiguration : IEntityTypeConfiguration<EventPrice>
    {
        public void Configure(EntityTypeBuilder<EventPrice> builder)
        {
            builder.HasKey(x => x.EventPriceId);

            builder.HasOne(x => x.Event)
                .WithMany(x => x.EventPrices)
                .HasForeignKey(x => x.EventId);

            builder.HasOne(x => x.Zone)
                .WithMany(x => x.EventPrices)
                .HasForeignKey(x => x.ZoneId);
        }
    }
}
