using BookingSystem.DTO.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BookingConsoleApp
{
    private readonly ApiClient _apiClient;

    public BookingConsoleApp(string baseUrl)
    {
        _apiClient = new ApiClient(baseUrl);
    }

    public async Task RunAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Skapa bokning");
            Console.WriteLine("2. Lista bokningar för rum");
            Console.WriteLine("3. Kontrollera tillgänglighet");
            Console.WriteLine("4. Lista alla kunder");
            Console.WriteLine("5. Lista alla rum");
            Console.WriteLine("6. Lista alla bokningar");
            Console.WriteLine("7. Lägg till kund");
            Console.WriteLine("8. Ta bort kund");
            Console.WriteLine("9. Lägg till rum");
            Console.WriteLine("10. Ta bort rum");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": await CreateBookingAsync(); break;
                case "2": await ListBookingsForRoomAsync(); break;
                case "3": await CheckRoomAvailabilityAsync(); break;
                case "4": await ListAllCustomersAsync(); break;
                case "5": await ListAllRoomsAsync(); break;
                case "6": await ListAllBookingsAsync(); break;
                case "7": await AddCustomerAsync(); break;
                case "8": await DeleteCustomerAsync(); break;
                case "9": await AddRoomAsync(); break;
                case "10": await DeleteRoomAsync(); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Ogiltigt val"); break;
            }

            Console.WriteLine();
        }
    }

    private async Task CreateBookingAsync()
    {
        var rooms = await _apiClient.GetAllRoomsAsync();
        var customers = await _apiClient.GetAllCustomersAsync();

        Console.WriteLine("Tillgängliga rum:");
        foreach (var r in rooms)
            Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
        Console.Write("Välj RoomId: ");
        int roomId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Tillgängliga kunder:");
        foreach (var c in customers)
            Console.WriteLine($"CustomerId: {c.CustomerId}, Name: {c.Name}");
        Console.Write("Välj CustomerId: ");
        int customerId = int.Parse(Console.ReadLine()!);

        Console.Write("Datum (yyyy-MM-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!);
        var start = date.Date;
        var end = date.Date;

        var bookingDto = new BookingDto
        {
            RoomId = roomId,
            CustomerId = customerId,
            StartDate = DateOnly.FromDateTime(start),
            EndDate = DateOnly.FromDateTime(end)
        };

        var created = await _apiClient.CreateBookingAsync(bookingDto);
        Console.WriteLine($"Bokning skapad med Id: {created.BookingId} för datum {date:yyyy-MM-dd}");
    }

    private async Task ListBookingsForRoomAsync()
    {
        var rooms = await _apiClient.GetAllRoomsAsync();
        foreach (var r in rooms)
            Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
        Console.Write("Välj RoomId: ");
        int roomId = int.Parse(Console.ReadLine()!);

        var bookings = await _apiClient.GetBookingsForRoomAsync(roomId);
        Console.Clear();
        foreach (var b in bookings)
            Console.WriteLine($"BookingId: {b.BookingId}, CustomerId: {b.CustomerId}, Start: {b.StartDate:yyyy-MM-dd}, End: {b.EndDate:yyyy-MM-dd}");
    }

    private async Task CheckRoomAvailabilityAsync()
    {
        var rooms = await _apiClient.GetAllRoomsAsync();
        foreach (var r in rooms)
            Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
        Console.Write("Välj RoomId: ");
        int roomId = int.Parse(Console.ReadLine()!);

        Console.Write("Datum (yyyy-MM-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!);
        var start = date.Date;
        var end = date.Date;

        bool available = await _apiClient.IsRoomAvailableAsync(roomId, start, end);
        Console.WriteLine(available ? "Rummet är ledigt" : "Rummet är upptaget");
    }

    private async Task ListAllCustomersAsync()
    {
        var customers = await _apiClient.GetAllCustomersAsync();
        Console.Clear();
        Console.WriteLine("Alla kunder:");
        foreach (var c in customers)
            Console.WriteLine($"CustomerId: {c.CustomerId}, Name: {c.Name}");
    }

    private async Task ListAllRoomsAsync()
    {
        var rooms = await _apiClient.GetAllRoomsAsync();
        Console.Clear();
        Console.WriteLine("Alla rum:");
        foreach (var r in rooms)
            Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
    }

    private async Task ListAllBookingsAsync()
    {
        var bookings = await _apiClient.GetAllBookingsAsync();
        Console.Clear();
        Console.WriteLine("Alla bokningar:");
        foreach (var b in bookings)
            Console.WriteLine($"BookingId: {b.BookingId}, CustomerId: {b.CustomerId}, Start: {b.StartDate:yyyy-MM-dd}, End: {b.EndDate:yyyy-MM-dd}");
    }

    private async Task AddCustomerAsync()
    {
        Console.Write("Kundens namn: ");
        string name = Console.ReadLine()!;
        var newCustomer = await _apiClient.CreateCustomerAsync(new CustomerDto { Name = name });
        Console.Clear();
        Console.WriteLine($"Kund skapad med Id: {newCustomer.CustomerId}, Namn: {newCustomer.Name}");
    }

    private async Task DeleteCustomerAsync()
    {
        var customers = await _apiClient.GetAllCustomersAsync();
        foreach (var c in customers)
            Console.WriteLine($"CustomerId: {c.CustomerId}, Name: {c.Name}");
        Console.Write("CustomerId att ta bort: ");
        int customerId = int.Parse(Console.ReadLine()!);
        bool deleted = await _apiClient.DeleteCustomerAsync(customerId);
        Console.WriteLine(deleted ? "Kund borttagen." : "Kund hittades inte.");
    }

    private async Task AddRoomAsync()
    {
        Console.Write("Rummets namn: ");
        string name = Console.ReadLine()!;
        var newRoom = await _apiClient.CreateRoomAsync(new RoomDto { Name = name });
        Console.Clear();
        Console.WriteLine($"Rum skapad med Id: {newRoom.RoomId}, Namn: {newRoom.Name}");
    }

    private async Task DeleteRoomAsync()
    {
        var rooms = await _apiClient.GetAllRoomsAsync();
        foreach (var r in rooms)
            Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
        Console.Write("RoomId att ta bort: ");
        int roomId = int.Parse(Console.ReadLine()!);
        bool deleted = await _apiClient.DeleteRoomAsync(roomId);
        Console.WriteLine(deleted ? "Rum borttaget." : "Rum hittades inte.");
    }
}
