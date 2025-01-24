using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Data;

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

        // Edit: Return NotFound when ID is missing
        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        // Edit: Return NotFound when HealthData is missing
        [Fact]
        public async Task Edit_should_return_notfound_when_healthdata_is_missing()
        {
            // Arrange
            int id = 1;
            _healthDataServiceMock.Setup(x => x.Get(id)).ReturnsAsync((HealthData)null);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        // Edit: Return View with model when HealthData is found
        [Fact]
        public async Task Edit_should_return_view_with_model_when_healthdata_was_found()
        {
            // Arrange
            int id = 1;
            var healthData = new HealthData
            {
                Id = id,
                User = "Test User",  // Using 'User' instead of 'Name'
                Date = "2025-01-01",
                BloodSugar = 5.6f,
                Weight = 70.0f
            };
            _healthDataServiceMock.Setup(x => x.Get(id)).ReturnsAsync(healthData);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(healthData, result.Model);
        }

        // Add: Test adding health data
        [Fact]
        public async Task Add_should_return_view_when_healthdata_is_added()
        {
            // Arrange
            var healthData = new HealthData
            {
                Id = 1,
                User = "Test User",      // Using 'User' instead of 'Name'
                Date = "2025-01-01",
                BloodSugar = 5.6f,
                Weight = 70.0f
            };
            _healthDataServiceMock.Setup(x => x.Add(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Add(healthData) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);  // Ensure correct redirection
        }

        // Delete: Return NotFound when HealthData is missing
        [Fact]
        public async Task Delete_should_return_notfound_when_healthdata_is_missing()
        {
            // Arrange
            int id = 1;
            _healthDataServiceMock.Setup(x => x.Get(id)).ReturnsAsync((HealthData)null);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        // Delete: Return View with model when HealthData is found
        [Fact]
        public async Task Delete_should_return_view_when_healthdata_is_found()
        {
            // Arrange
            int id = 1;
            var healthData = new HealthData
            {
                Id = id,
                User = "Test User",  // Using 'User' instead of 'Name'
                Date = "2025-01-01",
                BloodSugar = 5.6f,
                Weight = 70.0f
            };
            _healthDataServiceMock.Setup(x => x.Get(id)).ReturnsAsync(healthData);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(healthData, result.Model);
        }
    }
}
