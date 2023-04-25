﻿using EventTicketing.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketing.Application.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(x => x.TicketId);

            builder.HasOne(x => x.EventPrice)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.EventPriceId);

            builder.HasOne(x => x.Place)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.TicketId);

            builder.Property(x => x.ParticipantName)
                .HasMaxLength(90);
        }
    }
}
