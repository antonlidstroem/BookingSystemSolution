using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var app = new BookingConsoleApp("https://localhost:7262/");
        await app.RunAsync();
    }
}
