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
    public class NutrientControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public NutrientControllerTests()
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
            using var response = await _client.GetAsync("/Nutrients");

            // Assert
            response.EnsureSuccessStatusCode();
        }

     
        
        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Nutrients/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new Nutrients { Title = "Test" };
            _context.nutrients.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Nutrients/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Id", "0");
            formValues.Add("Title", "Test");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Nutrients/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.nutrients.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Title);
        }
        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange: Send an empty title (this should trigger a validation failure in the model)
            var formValues = new Dictionary<string, string>
    {
        { "Title", "" } // Empty Title which is invalid
    };

            using var content = new FormUrlEncodedContent(formValues);

            // Act: Post to create a new Nutrient
            using var response = await _client.PostAsync("/Nutrients/Create", content);

            // Assert: Ensure a 400 Bad Request is returned due to validation failure
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // Assert: Ensure no new entries are added to the database
            Assert.False(_context.nutrients.Any());
        }

    }
}