using BookingSystem.DTO.DTO;
using System.Net.Http.Json;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    // Bokningar
    public async Task<List<BookingDto>> GetAllBookingsAsync() =>
        await _http.GetFromJsonAsync<List<BookingDto>>("api/bookings") ?? new List<BookingDto>();

    public async Task<BookingDto?> GetBookingByIdAsync(int id) =>
        await _http.GetFromJsonAsync<BookingDto?>($"api/bookings/{id}");

    public async Task<BookingDto> CreateBookingAsync(BookingDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/bookings", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BookingDto>()!;
    }

    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end)
    {
        var url = $"api/bookings/available?roomId={roomId}&start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<bool>(url);
    }

    public async Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId) =>
        await _http.GetFromJsonAsync<List<BookingDto>>($"api/bookings/room/{roomId}") ?? new List<BookingDto>();

    // Kunder
    public async Task<List<CustomerDto>> GetAllCustomersAsync() =>
        await _http.GetFromJsonAsync<List<CustomerDto>>("api/customers") ?? new List<CustomerDto>();

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/customers", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomerDto>()!;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/customers/{id}");
        return response.IsSuccessStatusCode;
    }

    // Rum
    public async Task<List<RoomDto>> GetAllRoomsAsync() =>
        await _http.GetFromJsonAsync<List<RoomDto>>("api/rooms") ?? new List<RoomDto>();

    public async Task<RoomDto> CreateRoomAsync(RoomDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/rooms", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RoomDto>()!;
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/rooms/{id}");
        return response.IsSuccessStatusCode;
    }
}
