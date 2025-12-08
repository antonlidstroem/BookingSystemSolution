using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookingSystem.Console.Interface;
using BookingSystem.Console.Services;

// Skapa HttpClient med basadress till API
var http = new HttpClient { BaseAddress = new Uri("https://localhost:7262/") };
IBookingService bookingService = new BookingService(http);

// Exempelanvändning
var bookings = await bookingService.GetAllBookingsAsync();
Console.WriteLine("Befintliga bokningar:");
foreach (var b in bookings)
{
    Console.WriteLine($"{b.BookingId}: Room {b.RoomId}, Customer {b.CustomerId}, {b.StartTime} - {b.EndTime}");
}

