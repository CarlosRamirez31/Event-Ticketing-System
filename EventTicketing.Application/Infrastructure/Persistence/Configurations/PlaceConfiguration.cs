using EventTicketing.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketing.Application.Infrastructure.Persistence.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(x => x.PlaceId);

            builder.HasOne(x => x.Zone)
                .WithMany(x => x.Places)
                .HasPrincipalKey(x => x.ZoneId);

            builder.Property(x => x.Name).HasMaxLength(90);
        }
    }
}
