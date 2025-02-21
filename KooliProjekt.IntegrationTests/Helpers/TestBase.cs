using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public abstract class TestBase : IDisposable
    {
        public WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase()
        {
            Factory = new TestApplicationFactory<FakeStartup>();
        }

        public void Dispose()
        {
            var dbContext = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
            dbContext?.Database.EnsureDeleted();
        }

        // Add your other helper methods here if necessary
    }
}
