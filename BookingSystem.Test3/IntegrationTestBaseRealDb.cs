using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookingSystem.Test3
{
    public abstract class IntegrationTestBaseRealDb 
        : IClassFixture<WebApplicationFactory<BookingSystem.API.Program>>
    {
        protected readonly HttpClient HttpClient;

        protected IntegrationTestBaseRealDb(
            WebApplicationFactory<BookingSystem.API.Program> factory)
        {
            HttpClient = factory.CreateDefaultClient();
        }
    }
}





