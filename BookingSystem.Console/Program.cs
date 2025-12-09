using BookingSystem.DTO.DTO;
using System.Net.Http.Json;

var apiClient = new ApiClient("https://localhost:7262/");
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
        case "1":
            Console.Write("RoomId: ");
            int roomId = int.Parse(Console.ReadLine()!);
            Console.Write("Datum (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine()!);
            DateTime start = date.Date;
            DateTime end = date.Date.AddDays(1).AddSeconds(-1);

            Console.Write("CustomerId: ");
            int customerId = int.Parse(Console.ReadLine()!);

            var bookingDto = new BookingDto
            {
                RoomId = roomId,
                StartTime = start,
                EndTime = end,
                CustomerId = customerId
            };
            var created = await apiClient.CreateBookingAsync(bookingDto);
            Console.WriteLine($"Bokning skapad med Id: {created.BookingId} för datum {date:yyyy-MM-dd}");
            break;

        case "2":
            Console.Write("RoomId: ");
            int roomListId = int.Parse(Console.ReadLine()!);
            var bookings = await apiClient.GetBookingsForRoomAsync(roomListId);
            Console.Clear();
            foreach (var b in bookings)
                Console.WriteLine($"BookingId: {b.BookingId}, CustomerId: {b.CustomerId}, Start: {b.StartTime:yyyy-MM-dd}, End: {b.EndTime:yyyy-MM-dd}");
            break;

        case "3":
            Console.Write("RoomId: ");
            int roomCheckId = int.Parse(Console.ReadLine()!);
            Console.Write("Datum (yyyy-MM-dd): ");
            DateTime checkDate = DateTime.Parse(Console.ReadLine()!);
            DateTime startCheck = checkDate.Date;
            DateTime endCheck = checkDate.Date.AddDays(1).AddSeconds(-1);

            bool available = await apiClient.IsRoomAvailableAsync(roomCheckId, startCheck, endCheck);
            Console.WriteLine(available ? "Rummet är ledigt" : "Rummet är upptaget");
            break;

        case "4":
            var customers = await apiClient.GetAllCustomersAsync();
            Console.Clear();
            Console.WriteLine("Alla kunder:");
            foreach (var c in customers)
                Console.WriteLine($"CustomerId: {c.CustomerId}, Name: {c.Name}");
            break;

        case "5":
            var rooms = await apiClient.GetAllRoomsAsync();
            Console.Clear();
            Console.WriteLine("Alla rum:");
            foreach (var r in rooms)
                Console.WriteLine($"RoomId: {r.RoomId}, Name: {r.Name}");
            break;

        case "6":
            var allBookings = await apiClient.GetAllBookingsAsync();
            Console.Clear();
            Console.WriteLine("Alla bokningar:");
            foreach (var b in allBookings)
                Console.WriteLine($"BookingId: {b.BookingId}, RoomId: {b.RoomId}, CustomerId: {b.CustomerId}, Start: {b.StartTime:yyyy-MM-dd}, End: {b.EndTime:yyyy-MM-dd}");
            break;

        case "7": // Lägg till kund
            Console.Write("Kundens namn: ");
            string name = Console.ReadLine()!;
            var newCustomer = await apiClient.CreateCustomerAsync(new CustomerDto { Name = name });
            Console.Clear();
            Console.WriteLine($"Kund skapad med Id: {newCustomer.CustomerId}, Namn: {newCustomer.Name}");
            break;

        case "8": // Ta bort kund
            Console.Write("CustomerId att ta bort: ");
            int delCustomerId = int.Parse(Console.ReadLine()!);
            bool deletedCustomer = await apiClient.DeleteCustomerAsync(delCustomerId);
            Console.WriteLine(deletedCustomer ? "Kund borttagen." : "Kund hittades inte.");
            break;

        case "9": // Lägg till rum
            Console.Write("Rummets namn: ");
            string roomName = Console.ReadLine()!;
            var newRoom = await apiClient.CreateRoomAsync(new RoomDto { Name = roomName });
            Console.Clear();
            Console.WriteLine($"Rum skapad med Id: {newRoom.RoomId}, Namn: {newRoom.Name}");
            break;

        case "10": // Ta bort rum
            Console.Write("RoomId att ta bort: ");
            int delRoomId = int.Parse(Console.ReadLine()!);
            bool deletedRoom = await apiClient.DeleteRoomAsync(delRoomId);
            Console.Clear();
            Console.WriteLine(deletedRoom ? "Rum borttaget." : "Rum hittades inte.");
            break;

        case "0":
            exit = true;
            break;

        default:
            Console.WriteLine("Ogiltigt val");
            break;
    }

    Console.WriteLine(); 
}
