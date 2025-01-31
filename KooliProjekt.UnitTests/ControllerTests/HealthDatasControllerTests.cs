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

        // Testing the Index method
        [Fact]
        public async Task Index_Should_Return_View_With_Data()
        {
            var healthDataList = new List<HealthData>
            {
                new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f },
                new HealthData { Id = 2, User = "User 2", Date = "2025-02-01", BloodSugar = 5.7f, Weight = 71.0f }
            };

            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthDataList[0]);
            _healthDataServiceMock.Setup(service => service.Get(2)).ReturnsAsync(healthDataList[1]);

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<HealthData>;

            Assert.NotNull(result);
            Assert.Equal(healthDataList, model);
        }

        // Testing the Create method (GET)
        [Fact]
        public void Create_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        // Testing the Create method (POST)
        [Fact]
        public async Task Create_Should_Return_View_When_ModelState_Is_Invalid()
        {
            _controller.ModelState.AddModelError("User", "Required");
            var healthData = new HealthData();

            var result = await _controller.Create(healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Create_Should_Redirect_To_Index_When_HealthData_Is_Valid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Add(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            var result = await _controller.Create(healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Testing the Edit method (GET)
        [Fact]
        public async Task Edit_Should_Return_View_With_Model_When_HealthData_Is_Found()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            var result = await _controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_HealthData_Is_Missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.Edit(1) as NotFoundResult;

            Assert.NotNull(result);
        }

        // Testing the Edit method (POST)
        [Fact]
        public async Task Edit_Should_Return_View_When_ModelState_Is_Invalid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _controller.ModelState.AddModelError("User", "Required");

            var result = await _controller.Edit(1, healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Edit_Should_Redirect_To_Index_When_HealthData_Is_Valid()
        {
            var healthData = new HealthData { Id = 1, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Update(It.IsAny<HealthData>())).Returns(Task.CompletedTask);

            var result = await _controller.Edit(1, healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_Id_Mismatch()
        {
            var healthData = new HealthData { Id = 2, User = "Test User", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };

            var result = await _controller.Edit(1, healthData) as NotFoundResult;

            Assert.NotNull(result);
        }

        // Testing the Delete method (GET)
        [Fact]
        public async Task Delete_Should_Return_NotFound_When_HealthData_Is_Missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.Delete(1) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_View_With_Model_When_HealthData_Is_Found()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            var result = await _controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        // Testing the DeleteConfirmed method
        [Fact]
        public async Task DeleteConfirmed_Should_Redirect_To_Index_When_Successful()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);
            _healthDataServiceMock.Setup(service => service.Delete(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Return_NotFound_When_HealthData_Is_Missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.DeleteConfirmed(1) as NotFoundResult;

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        // Testing the Details method
        [Fact]
        public async Task Details_Should_Return_View_With_Model_When_HealthData_Is_Found()
        {
            var healthData = new HealthData { Id = 1, User = "User 1", Date = "2025-01-01", BloodSugar = 5.6f, Weight = 70.0f };
            _healthDataServiceMock.Setup(service => service.Get(1)).ReturnsAsync(healthData);

            var result = await _controller.Details(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result?.Model);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_HealthData_Is_Missing()
        {
            _healthDataServiceMock.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((HealthData)null);

            var result = await _controller.Details(1) as NotFoundResult;

            Assert.NotNull(result);
        }
    }
}
