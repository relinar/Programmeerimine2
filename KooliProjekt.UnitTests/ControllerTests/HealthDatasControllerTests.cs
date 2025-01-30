using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async Task Index_should_return_view_with_data()
        {
            var healthDataList = new List<HealthData>
            {
                new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f },
                new HealthData { Id = 2, User = "User 2", Date = "2025-02-01", BloodSugar = 5.7f, Weight = 71.0f }
            };
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((int id) => healthDataList.FirstOrDefault(h => h.Id == id));

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<HealthData>;

            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Create_should_return_view()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_should_return_view_when_modelstate_is_invalid()
        {
            _controller.ModelState.AddModelError("User", "Required");
            var healthData = new HealthData();

            var result = await _controller.Create(healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_health_data_is_valid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Add(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            var result = await _controller.Create(healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_health_data_is_found()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            var result = await _controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_health_data_is_missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.Edit(1) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_modelstate_is_invalid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _controller.ModelState.AddModelError("User", "Required");

            var result = await _controller.Edit(1, healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Edit_should_redirect_to_index_when_health_data_is_valid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Update(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            var result = await _controller.Edit(1, healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_mismatch()
        {
            var healthData = new HealthData { Id = 2, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };

            var result = await _controller.Edit(1, healthData) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_health_data_is_missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.Delete(1) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_health_data_is_found()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            var result = await _controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_redirect_to_index_when_successful()
        {
            _healthDataServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_notfoundresult()
        {
            // Arrange
            int id = 1;
            // Act
            _healthDataServiceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null).Verifiable();
            var result = await _controller.Edit(id);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            _healthDataServiceMock.VerifyAll();
        }
        [Fact]
        public async Task Create_should_return_view_when_ModelState_is_invalid_and_return_correct_view()
            {
                // Arrange
                var healthData = new HealthData();
                _controller.ModelState.AddModelError("User", "Required");

                // Act
                var result = await _controller.Create(healthData) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(healthData, result?.Model);
            }

        [Fact]
        public async Task Add_should_return_view_when_ModelState_is_invalid()
        {
            // Arrange
            var healthData = new HealthData();
            _controller.ModelState.AddModelError("User", "Required");

            // Act
            var result = await _controller.Create(healthData) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Add_should_redirect_to_index_when_ModelState_is_valid()
        {
            // Arrange
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Add(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(healthData) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}