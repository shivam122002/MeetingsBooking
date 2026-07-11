using MeetingsBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingsBooking.Infrastructure.Persistence.Configurations;

public class MeetingConfiguration
    : IEntityTypeConfiguration<Meetings>
{
    public void Configure(
        EntityTypeBuilder<Meetings> builder)
    {
        builder.ToTable("Meetings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MeetingDate)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.DurationInMinutes)
            .IsRequired();

        builder.Property(x => x.TimeZone)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.MeetingDetailsJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.Status)
       .HasConversion<int>()
       .IsRequired();
    }
}