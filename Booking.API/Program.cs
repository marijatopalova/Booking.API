using Booking.API.Authorization;
using Booking.API.Services;
using Booking.Domain.Entities;
using Booking.Domain.ExceptionHandling;
using Booking.Domain.Interfaces;
using Booking.Infrastructure;
using Booking.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddDbContext<BookDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookDB"));

var app = builder.Build();
SeedOptions(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedOptions(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<BookDbContext>();

    var options = new List<Option>()
            {
                new Option() { OptionCode = "1234", ArrivalAirport = "SKP", FlightCode = "1111", HotelCode = "1212", Price = 99},
                new Option() { OptionCode = "2345", ArrivalAirport = "MLH", FlightCode = "2222", HotelCode = "1313", Price = 100},
                new Option() { OptionCode = "3456", ArrivalAirport = "STO", FlightCode = "3333", HotelCode = "1414", Price = 59},
                new Option() { OptionCode = "4567", ArrivalAirport = "MLH", FlightCode = "4444", HotelCode = "1515", Price = 63},
                new Option() { OptionCode = "5678", ArrivalAirport = "NKJ", FlightCode = "5555", HotelCode = "1616", Price = 32},
                new Option() { OptionCode = "6789", ArrivalAirport = "FKI", FlightCode = "6666", HotelCode = "1717", Price = 79},
                new Option() { OptionCode = "7890", ArrivalAirport = "SKP", FlightCode = "7777", HotelCode = "1818", Price = 45},
                new Option() { OptionCode = "8901", ArrivalAirport = "IMR", FlightCode = "8888", HotelCode = "1919", Price = 120},
            };

    db.Options.AddRange(options);
    db.SaveChanges();
}
