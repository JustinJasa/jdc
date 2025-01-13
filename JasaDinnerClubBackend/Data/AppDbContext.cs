using Microsoft.EntityFrameworkCore;
using JasaDinnerClubBackend.Models;

namespace JasaDinnerClubBackend.Data;

// more to add
public class AppDbContext : DbContext
{
    public DbSet<DinnerEvent> DinnerEvents { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Attendee> Attendee { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Attendee)
            .WithMany(a => a.Bookings)
            .HasForeignKey(b => b.AttendeeId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.DinnerEvent)
            .WithMany(d => d.Bookings)
            .HasForeignKey(b => b.DinnerId);

        modelBuilder.Entity<DinnerEvent>().HasKey(de => de.DinnerId);

    }
}
