namespace KooliProjekt.UnitTests.ServiceTests
{
    using KooliProjekt.Controllers;
    using KooliProjekt.Data;
    using KooliProjekt.Models;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class HealthDatasControllerTests : ServiceTestBase
    {
        private readonly HealthDatasController _controller;

        public HealthDatasControllerTests()
        {
            _controller = new HealthDatasController(_healthDataServiceMock.Object);
        }

        [Fact]
        public async Task Create_should_add_new_health_data()
        {
            // Arrange
            var healthData = new HealthData { User = "Test User", BloodSugar = 120 };
            _healthDataServiceMock.Setup(service => service.AddAsync(healthData)).Returns(Task.CompletedTask);
            _healthDataServiceMock.Setup(service => service.SaveAsync()).Returns(Task.CompletedTask); // No arguments

            // Act
            var result = await _controller.Create(healthData);

            // Assert
            _healthDataServiceMock.Verify(service => service.AddAsync(healthData), Times.Once);
            _healthDataServiceMock.Verify(service => service.SaveAsync(), Times.Once); // No arguments
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_should_remove_health_data()
        {
            // Arrange
            int id = 1;
            _healthDataServiceMock.Setup(service => service.DeleteAsync(id)).Returns(Task.CompletedTask);
            _healthDataServiceMock.Setup(service => service.SaveAsync()).Returns(Task.CompletedTask); // No arguments

            // Act
            var result = await _controller.DeleteConfirmed(id);

            // Assert
            _healthDataServiceMock.Verify(service => service.DeleteAsync(id), Times.Once);
            _healthDataServiceMock.Verify(service => service.SaveAsync(), Times.Once); // No arguments
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
