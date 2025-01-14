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
app.MapGet("/dinners/{id}/bookings", async (int id, AppDbContext db) => await db.Bookings.Where(b => b.DinnerId == id).ToListAsync());
app.MapGet("/dinners/{id}/attendees", async (int id, AppDbContext db) =>
{
    var attendees = await db.Bookings
        .Where(b => b.DinnerId == id)          // Filter by DinnerID
        .Select(b => b.Attendee)               // Select the related Attendees
        .ToListAsync();                        // Execute the query and retrieve as a list

    return attendees.Any() ? Results.Ok(attendees) : Results.NotFound();
});
app.MapGet("/dinners/{id}/capacity", async (int id, AppDbContext db) => await db.DinnerEvents.FindAsync(id) is DinnerEvent dinner ? Results.Ok(dinner.Capacity) : Results.NotFound());
app.MapPost("/dinners", async (DinnerEvent dinner, AppDbContext db) =>
{
    // Validate the input model
    if (string.IsNullOrWhiteSpace(dinner.Name))
        return Results.BadRequest("Dinner Name is required.");
    if (dinner.Date == default)
        return Results.BadRequest("Dinner Date is required.");
    if (dinner.Time == default)
        return Results.BadRequest("Dinner Time is required.");
    if (dinner.Capacity <= 0)
        return Results.BadRequest("Capacity must be greater than zero.");
    if (string.IsNullOrWhiteSpace(dinner.Description))
        return Results.BadRequest("Description is required.");

    // Add the dinner to the database
    db.DinnerEvents.Add(dinner);
    await db.SaveChangesAsync();

    // Return a Created result
    return Results.Created($"/dinners/{dinner.DinnerId}", dinner);
});

//Attendees Requests
app.MapGet("/attendees", async (AppDbContext db) => await db.Attendee.ToListAsync());
app.MapGet("/attendees/{id}", async (int id, AppDbContext db) => await db.Attendee.FindAsync(id) is Attendee attendee ? Results.Ok(attendee) : Results.NotFound());
app.MapGet("/attendees/{id}/bookings", async (int id, AppDbContext db) =>
{
    var attendeeWithBookings = await db.Bookings
        .Where(a => a.AttendeeId == id)
        .ProjectToType<BookingDto>() // Mapster projection
        .FirstOrDefaultAsync();

    return attendeeWithBookings is not null ? Results.Ok(attendeeWithBookings) : Results.NotFound();
});
app.MapGet("/attendees/{id}/dinners", async (int id, AppDbContext db) =>
{
    var dinners = await db.Bookings
        .Where(b => b.AttendeeId == id) // Filter by attendee ID
        .Select(b => b.DinnerEvent)     // Select the related DinnerEvent
        .ProjectToType<DinnerEventDto>() // Use Mapster to map to DinnerEventDto
        .ToListAsync();                 // Execute the query and get the results as a list

    return dinners.Any() ? Results.Ok(dinners) : Results.NotFound();
});


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
