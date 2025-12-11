using BookingSystem.DAL.Data;
using BookingSystem.Test3;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class IntegrationTestBase : IDisposable
{
    protected readonly WebApplicationFactory<BookingSystem.API.Program> WebAppFactory;
    protected readonly HttpClient HttpClient;
    private readonly string _inMemoryDbName = Guid.NewGuid().ToString(); // unik per instans

    public IntegrationTestBase()
    {
        WebAppFactory = new WebApplicationFactory<BookingSystem.API.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    var descriptors = services
                        .Where(d => d.ServiceType == typeof(DbContextOptions<BookingSystemAPIContext>))
                        .ToList();
                    foreach (var d in descriptors)
                        services.Remove(d);

                    services.AddDbContext<BookingSystemAPIContext>(options =>
                    {
                        options.UseInMemoryDatabase(_inMemoryDbName); // unik
                    });
                });
            });

        HttpClient = WebAppFactory.CreateDefaultClient();

        using var scope = WebAppFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BookingSystemAPIContext>();
        context.Database.EnsureCreated();
        SeedHelper.SeedDatabase(context);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var scope = WebAppFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BookingSystemAPIContext>();
        context.Database.EnsureDeleted();
    }
}

