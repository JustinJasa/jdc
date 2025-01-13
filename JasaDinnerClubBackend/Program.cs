using JasaDinnerClubBackend.Data;
using Microsoft.EntityFrameworkCore;
using JasaDinnerClubBackend.Models;
using Mapster;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Map Endpoints

//Booking Requests
app.MapGet("/bookings", async (AppDbContext db) => await db.Bookings.ToListAsync());
app.MapGet("/bookings/{id}", async (int id, AppDbContext db) =>
{
    var booking = await db.Bookings
                          .Include(b => b.DinnerEvent)
                          .Include(b => b.Attendee)
                          .Where(b => b.BookingId == id)
                          .ProjectToType<BookingDto>() // Use Mapster's projection
                          .FirstOrDefaultAsync();

    return booking is not null ? Results.Ok(booking) : Results.NotFound();
});

//Dinner Requests
app.MapGet("/dinners", async (AppDbContext db) => await db.DinnerEvents.ToListAsync());
app.MapGet("/dinners/{id}", async (int id, AppDbContext db) => await db.DinnerEvents.FindAsync(id) is DinnerEvent dinner ? Results.Ok(dinner) : Results.NotFound());


//Attendees Requests
app.MapGet("/attendees", async (AppDbContext db) => await db.Attendee.ToListAsync());
app.MapGet("/attendees/{id}", async (int id, AppDbContext db) => await db.Attendee.FindAsync(id) is Attendee attendee ? Results.Ok(attendee) : Results.NotFound());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // db.Database.Migrate(); // Automatically apply migrations
}

app.Run();
