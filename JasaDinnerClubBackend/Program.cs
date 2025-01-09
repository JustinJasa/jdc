using JasaDinnerClubBackend.Data;
using JasaDinnerClubBackend.Models;
using Microsoft.EntityFrameworkCore;


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
app.MapGet("/dinners", async (AppDbContext db) => await db.Bookings.ToListAsync());
app.MapGet("/attendees", async (AppDbContext db) => await db.Bookings.ToListAsync());


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
