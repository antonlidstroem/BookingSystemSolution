using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Repositories;
using BookingSystem.API.Interface;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookingSystemAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingSystemAPIContext") ?? throw new InvalidOperationException("Connection string 'BookingSystemAPIContext' not found.")));

// DAL (Repository)
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Service-lagret (API-logik)
builder.Services.AddScoped<IBookingService, BookingService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
