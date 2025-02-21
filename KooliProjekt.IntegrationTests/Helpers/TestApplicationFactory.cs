using Microsoft.Extensions.DependencyInjection;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using KooliProjekt.Data;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public class TestApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder()
                            .ConfigureWebHost(builder =>
                            {
                                builder.UseContentRoot(Directory.GetCurrentDirectory());
                                builder.ConfigureAppConfiguration((context, config) =>
                                {
                                    config.AddJsonFile("appsettings.json"); // Ensure configuration is loaded
                                });

                                builder.ConfigureServices(services =>
                                {
                                    var mockAmountService = new Mock<IAmountService>();
                                    // Setup mock behavior here if needed
                                    mockAmountService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Amount)null); // Ensure it returns null for 100

                                    // Register the mock service
                                    services.AddScoped<IAmountService>(provider => mockAmountService.Object);
                                });

                                builder.UseStartup<TTestStartup>(); // Use the actual startup class or test one
                            });

            return host;
        }
    }
}
