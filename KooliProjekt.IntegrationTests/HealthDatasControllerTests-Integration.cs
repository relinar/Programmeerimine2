using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class HealthDataControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public HealthDataControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]

        public async Task Index_should_return_success()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/HealthDatas");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/HealthDatas/Details")]
        [InlineData("/HealthDatas/Details/100")]
        [InlineData("/HealthDatas/Delete")]
        [InlineData("/HealthDatas/Delete/100")]
        [InlineData("/HealthDatas/Edit")]
        [InlineData("/HealthDatas/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/HealthDatas/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new HealthData { Title = "Test" };
            _context.health_data.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/HealthDatas/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange: Set up form values
            var formValues = new Dictionary<string, string>
{
    { "UserId", "1" },       // Ensure a valid UserId (assuming 1 exists)
    { "User", "TestUser" },  // Example user name
    { "Date", "2025-03-06" }, // Example date format
    { "BloodSugar", "5.5" },  // Sample float value
    { "Weight", "70.0" },     // Sample weight
    { "BloodAir", "98%" },    // Sample blood oxygen level
    { "Systolic", "120" },    // Sample systolic pressure
    { "Diastolic", "80" },    // Sample diastolic pressure
    { "Pulse", "72" },        // Sample pulse
    { "Height", "175" },      // Example height
    { "Age", "30" },          // Example age
    { "Title", "Test Entry" } // Ensure Title is provided
};

            using var content = new FormUrlEncodedContent(formValues);

            // Act: Submit form data to the controller
            using var response = await _client.PostAsync("/HealthDatas/Create", content);

            // Assert: Check if the response status is a redirection (302)
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            // Assert: Check if the Amount entity was created
            var amount = _context.amount.FirstOrDefault();  // Query the Amount entity table
            Assert.NotNull(amount);  // Assert that the Amount entity exists
            Assert.NotEqual(0, amount.AmountID);  // Ensure AmountID is not 0
            Assert.Equal("Test", amount.AmountTitle);  // Ensure AmountTitle is set
        }


        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
    {
        { "Title", "" } // Invalid Title (empty string)
    };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/HealthDatas/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.Found, response.StatusCode); // Expecting a redirect (302)

            // Ensure no new entries were created in the database
            var countBefore = _context.health_data.Count();
            Assert.Equal(1, countBefore); // No new entries should be created
        }

    }
}