using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookingSystem.DAL.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookingSystemAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingSystemAPIContext") ?? throw new InvalidOperationException("Connection string 'BookingSystemAPIContext' not found.")));

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
