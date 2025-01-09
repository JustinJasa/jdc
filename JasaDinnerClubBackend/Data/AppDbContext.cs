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
        // // Optional: Seed some initial data
        // modelBuilder.Entity<DinnerEvent>().HasData(
        //     new DinnerEvent { DinnerId = 1, Name = "Wine Night", Date = DateTime.Today, Time = TimeSpan.FromHours(19), Capacity = 20, Description = "Exclusive wine tasting" }
        // );

        modelBuilder.Entity<DinnerEvent>().HasKey(de => de.DinnerId);

        // Seed DinnerEvent data
        modelBuilder.Entity<DinnerEvent>().HasData(
            new DinnerEvent
            {
                DinnerId = 1,
                Name = "Wine Night",
                Date = DateTime.Today,
                Time = TimeSpan.FromHours(19),
                Capacity = 6,
                Description = "Exclusive wine tasting"
            }
        );

        // Optional: Seed related Booking data
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                BookingId = 1,
                DinnerId = 1, // Foreign key
                AttendeeId = 1, // Foreign key
                Request = "Vegetarian meal"
            }
        );

        // Optional: Seed Attendee data
        modelBuilder.Entity<Attendee>().HasData(
            new Attendee
            {
                AttendeeId = 1,
                AttendeeName = "John Doe",
                AttendeeNumber = "123-456-7890"
            }
        );
    }
}
