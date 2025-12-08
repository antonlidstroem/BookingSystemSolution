using BookingSystem.DTO.DTO;
using BookingSystem.Console.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingSystem.Console.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _http;

        public BookingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _http.GetFromJsonAsync<List<BookingDto>>("api/bookings");
            return bookings ?? new List<BookingDto>();
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<BookingDto>($"api/bookings/{id}");
        }

        public async Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId)
        {
            var all = await GetAllBookingsAsync();
            return all.FindAll(b => b.RoomId == roomId);
        }

        public async Task<(bool Success, string? Error, BookingDto? Booking)> CreateBookingAsync(BookingDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/bookings", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error, null);
            }

            var created = await response.Content.ReadFromJsonAsync<BookingDto>();
            return (true, null, created);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end)
        {
            var bookings = await GetBookingsForRoomAsync(roomId);

            foreach (var b in bookings)
            {
                if (start < b.EndTime && b.StartTime < end)
                    return false;
            }
            return true;
        }
    }
}
