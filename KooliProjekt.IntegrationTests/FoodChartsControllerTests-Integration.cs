using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Data.Migrations;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class FoodChartControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public FoodChartControllerTests()
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
            using var response = await _client.GetAsync("/FoodCharts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/FoodCharts/Details")]
        [InlineData("/FoodCharts/Details/100")]
        [InlineData("/FoodCharts/Delete")]
        [InlineData("/FoodCharts/Delete/100")]
        [InlineData("/FoodCharts/Edit")]
        [InlineData("/FoodCharts/Edit/100")]
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
            using var response = await _client.GetAsync("/FoodCharts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new Data.FoodChart
            {
                Title = "Test",
                InvoiceNo = "INV12345", // Set a valid InvoiceNo
                InvoiceDate = DateTime.UtcNow // Ensure InvoiceDate is set
            };
            _context.food_Chart.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/FoodCharts/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_list()
        {
            var formValues = new Dictionary<string, string>
{
    { "Id", "0" },
    { "Title", "Test" },
    { "InvoiceNo", "INV12345" },
    { "InvoiceDate", DateTime.UtcNow.ToString("yyyy-MM-dd") },
    { "Amount", "10" }  // Explicitly setting a nonzero value
};


            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/FoodCharts/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.food_Chart.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.amount);
            Assert.Equal("Test", list.Title);
            Assert.Equal("INV12345", list.InvoiceNo);
            Assert.Equal(DateTime.UtcNow.Date, list.InvoiceDate); // Comparing only dates to avoid precision issues
        }


        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("amount", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/FoodCharts/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.food_Chart.Any());
        }
    }
}