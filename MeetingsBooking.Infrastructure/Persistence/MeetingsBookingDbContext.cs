using MeetingsBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingsBooking.Infrastructure.Persistence;

public class MeetingsBookingDbContext : DbContext
{
    public MeetingsBookingDbContext(
        DbContextOptions<MeetingsBookingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Meetings> Meetings => Set<Meetings>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MeetingsBookingDbContext).Assembly);
    }
}