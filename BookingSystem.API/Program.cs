using BookingSystem.API.Interface;
using BookingSystem.API.Services;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookingSystemAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingSystemAPIContext") ?? throw new InvalidOperationException("Connection string 'BookingSystemAPIContext' not found.")));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();


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

namespace BookingSystem.API
{
    public partial class Program { } // tom klass för WebApplicationFactory
}
