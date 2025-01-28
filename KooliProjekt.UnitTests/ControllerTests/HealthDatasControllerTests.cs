using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HealthDatasControllerTests
    {
        private readonly Mock<IHealthDataService> _healthDataServiceMock;
        private readonly HealthDatasController _controller;

        public HealthDatasControllerTests()
        {
            _healthDataServiceMock = new Mock<IHealthDataService>();
            _controller = new HealthDatasController(_healthDataServiceMock.Object);
        }

        // Index: Test to return correct view with data
        [Fact]
        public async Task Index_should_return_view_with_data()
        {
            // Arrange: Prepare mock data
            var healthDataList = new List<HealthData>
            {
                new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f },
                new HealthData { Id = 2, User = "User 2", Date = "2025-02-01", BloodSugar = 5.7f, Weight = 71.0f }
            };

            // Mock the Get method
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthDataList[0]);
            _healthDataServiceMock.Setup(service => service.Get(2)).ReturnsAsync(healthDataList[1]);

            // Act: Call the Index action
            var result = await _controller.Index() as ViewResult;

            // Assert: Ensure that the view returns the correct model and data
            var model = result?.Model as List<HealthData>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        // Create: Test to return the create view
        [Fact]
        public void Create_should_return_view()
        {
            // Act: Call the Create action (GET)
            var result = _controller.Create() as ViewResult;

            // Assert: Ensure the result is a ViewResult
            Assert.NotNull(result);
        }

        // Create: Test creating a new health data record
        [Fact]
        public async Task Create_should_redirect_to_index_when_health_data_is_valid()
        {
            // Arrange: Create a new health data object
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Add(healthData)).Returns(Task.CompletedTask);

            // Act: Call the Create action (POST)
            var result = await _controller.Create(healthData) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Edit: Test editing an existing health data record (GET)
        [Fact]
        public async Task Edit_should_return_view_with_model_when_health_data_is_found()
        {
            // Arrange: Prepare a health data object
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            // Act: Call the Edit action
            var result = await _controller.Edit(1) as ViewResult;

            // Assert: Ensure the view returns the correct model
            Assert.NotNull(result);
            var model = result?.Model as HealthData;
            Assert.Equal(healthData, model);
        }

        // Edit: Test editing an existing health data record (POST)
        [Fact]
        public async Task Edit_should_redirect_to_index_when_health_data_is_valid()
        {
            // Arrange: Create a health data object to edit
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Update(healthData)).Returns(Task.CompletedTask);

            // Act: Call the Edit action (POST)
            var result = await _controller.Edit(1, healthData) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Delete: Test the Delete action (GET) should return NotFound when health data is not found
        [Fact]
        public async Task Delete_should_return_notfound_when_health_data_is_missing()
        {
            // Arrange: Health data with ID 1 does not exist
            int id = 1;
            _healthDataServiceMock.Setup(service => service.Get(id)).ReturnsAsync((HealthData)null);

            // Act: Call the Delete action
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert: Ensure it returns NotFound
            Assert.NotNull(result);
        }

        // Delete: Test the Delete action (GET) should return the correct view with model when health data is found
        [Fact]
        public async Task Delete_should_return_view_with_model_when_health_data_is_found()
        {
            // Arrange: Prepare a health data object
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            // Act: Call the Delete action
            var result = await _controller.Delete(1) as ViewResult;

            // Assert: Ensure the view returns the correct model
            Assert.NotNull(result);
            var model = result?.Model as HealthData;
            Assert.Equal(healthData, model);
        }

        // DeleteConfirmed: Test to ensure redirection after deleting
        [Fact]
        public async Task DeleteConfirmed_should_redirect_to_index_after_deleting_health_data()
        {
            // Arrange: Health data to delete
            int id = 1;
            _healthDataServiceMock.Setup(service => service.Delete(id)).Returns(Task.CompletedTask);

            // Act: Call the DeleteConfirmed action
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Details: Test the Details action (GET) should return NotFound when health data is missing
        [Fact]
        public async Task Details_should_return_notfound_when_health_data_is_missing()
        {
            // Arrange: Health data with ID 1 does not exist
            int id = 1;
            _healthDataServiceMock.Setup(service => service.Get(id)).ReturnsAsync((HealthData)null);

            // Act: Call the Details action
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert: Ensure it returns NotFound
            Assert.NotNull(result);
        }

        // Details: Test the Details action (GET) should return the correct view with model when health data is found
        [Fact]
        public async Task Details_should_return_view_with_model_when_health_data_is_found()
        {
            // Arrange: Prepare a health data object
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            // Act: Call the Details action
            var result = await _controller.Details(1) as ViewResult;

            // Assert: Ensure the view returns the correct model
            Assert.NotNull(result);
            var model = result?.Model as HealthData;
            Assert.Equal(healthData, model);
        }
    }
}
