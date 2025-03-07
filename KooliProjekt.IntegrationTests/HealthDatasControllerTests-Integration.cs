using System;
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
            // Arrange
            var formValues = new Dictionary<string, string>
    {
        { "UserId", "1" }, // Ensure UserId exists in database
        { "User", "TestUser" },
        { "Date", "2025-03-06" },
        { "BloodSugar", "5.5" },
        { "Weight", "70.0" },
        { "BloodAir", "98%" },
        { "Systolic", "120" },
        { "Diastolic", "80" },
        { "Pulse", "72" },
        { "Height", "175" },
        { "Age", "30" },
        { "Title", "Test Entry" }
    };
            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/HealthDatas/Create", content);

            // Log response for debugging
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {response.StatusCode}, Content: {responseContent}");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            // Verify database entry
            var healthData = _context.health_data.FirstOrDefault();
            Assert.NotNull(healthData);
            Assert.Equal("Test Entry", healthData.Title);
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