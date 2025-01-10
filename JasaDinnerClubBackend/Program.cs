using JasaDinnerClubBackend.Data;
using JasaDinnerClubBackend.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Map Endpoints

//Get Requests
app.MapGet("/bookings", async (AppDbContext db) => await db.Bookings.ToListAsync());
app.MapGet("/bookings/{id}", async (int id, AppDbContext db) =>
{
    // Fetch a booking with the specified ID
    var booking = await db.Bookings
                          .Include(b => b.DinnerEvent)  // Optional: Include related DinnerEvent
                          .Include(b => b.Attendee)    // Optional: Include related Attendee
                          .FirstOrDefaultAsync(b => b.BookingId == id);

    // Return 404 if booking is not found
    return booking is not null ? Results.Ok(booking) : Results.NotFound();
});

app.MapGet("/dinners", async (AppDbContext db) => await db.DinnerEvents.ToListAsync());
app.MapGet("/attendees", async (AppDbContext db) => await db.Attendee.ToListAsync());


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
