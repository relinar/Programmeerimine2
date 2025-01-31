using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Data;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class FoodChartsControllerTests
    {
        private readonly Mock<IFoodChartService> _foodChartServiceMock;
        private readonly FoodChartsController _controller;

        public FoodChartsControllerTests()
        {
            _foodChartServiceMock = new Mock<IFoodChartService>();
            _controller = new FoodChartsController(_foodChartServiceMock.Object);
        }

        // Testing the Index method
        [Fact]
        public async Task Index_Should_Return_View_With_FoodCharts()
        {
            var foodCharts = new List<FoodChart>
            {
                new FoodChart { Id = 1, InvoiceNo = "INV001", InvoiceDate = DateTime.Now, user = "User1", date = "2025-01-28", meal = "Meal1", nutrients = DateTime.Now, amount = 200.5f },
                new FoodChart { Id = 2, InvoiceNo = "INV002", InvoiceDate = DateTime.Now, user = "User2", date = "2025-01-28", meal = "Meal2", nutrients = DateTime.Now, amount = 150.5f }
            };

            _foodChartServiceMock.Setup(service => service.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<FoodChartSearch>()))
                .ReturnsAsync(new PagedResult<FoodChart>(
                    currentPage: 1,
                    pageSize: 5,
                    rowCount: foodCharts.Count,
                    results: foodCharts
                ));

            var result = await _controller.Index(1) as ViewResult;

            Assert.NotNull(result);
            var model = result.Model as FoodChartIndexModel;
            Assert.NotNull(model);
            Assert.Equal(foodCharts, model.Data.Results);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        // Testing the Details method
        [Fact]
        public async Task Details_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int? id = null;

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_FoodChart_Is_Missing()
        {
            int id = 1;
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync((FoodChart)null);

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_Should_Return_View_With_Model_When_FoodChart_Was_Found()
        {
            int id = 1;
            var foodChart = new FoodChart { Id = id };
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync(foodChart);

            var result = await _controller.Details(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(foodChart, result.Model);
        }

        // Testing the Create method (GET)
        [Fact]
        public void Create_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        // Testing the Create method (POST)
        [Fact]
        public async Task Create_Should_Redirect_To_Index_When_Valid()
        {
            var foodChart = new FoodChart { Id = 1 };
            _foodChartServiceMock.Setup(service => service.Save(It.IsAny<FoodChart>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(foodChart) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Create_Should_Return_View_When_ModelState_Is_Invalid()
        {
            _controller.ModelState.AddModelError("Id", "Required");
            var foodChart = new FoodChart();
            var result = await _controller.Create(foodChart) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(foodChart, result?.Model);
        }

        // Testing the Edit method (GET)
        [Fact]
        public async Task Edit_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int? id = null;

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_FoodChart_Is_Missing()
        {
            int id = 1;
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync((FoodChart)null);

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Should_Return_View_With_Model_When_FoodChart_Was_Found()
        {
            int id = 1;
            var foodChart = new FoodChart { Id = id };
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync(foodChart);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(foodChart, result.Model);
        }

        // Testing the Edit method (POST)
        [Fact]
        public async Task Edit_Should_Redirect_To_Index_When_Valid()
        {
            var foodChart = new FoodChart { Id = 1 };
            _foodChartServiceMock.Setup(service => service.Save(It.IsAny<FoodChart>())).Returns(Task.CompletedTask);
            var result = await _controller.Edit(foodChart.Id, foodChart) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_Should_Return_View_When_ModelState_Is_Invalid()
        {
            var foodChart = new FoodChart { Id = 1 };
            _controller.ModelState.AddModelError("user", "Required"); // Example model error

            var result = await _controller.Edit(foodChart.Id, foodChart) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(foodChart, result?.Model);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_Id_Mismatch()
        {
            var foodChart = new FoodChart { Id = 2 }; // Mismatched ID

            var result = await _controller.Edit(1, foodChart) as NotFoundResult;

            Assert.NotNull(result);
        }

        // Testing the Delete method (GET)
        [Fact]
        public async Task Delete_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int? id = null;

            var result = await _controller.Delete(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_FoodChart_Is_Missing()
        {
            int id = 1;
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync((FoodChart)null);

            var result = await _controller.Delete(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_View_With_Model_When_FoodChart_Was_Found()
        {
            int id = 1;
            var foodChart = new FoodChart { Id = id };
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync(foodChart);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(foodChart, result.Model);
        }

        // Testing the DeleteConfirmed method
        [Fact]
        public async Task DeleteConfirmed_Should_Redirect_To_Index_When_Valid()
        {
            int id = 1;
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync(new FoodChart { Id = id }); // Ensure a valid FoodChart is returned
            _foodChartServiceMock.Setup(service => service.Delete(id)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Return_NotFound_When_FoodChart_Is_Missing()
        {
            int id = 1;
            _foodChartServiceMock.Setup(x => x.Get(id)).ReturnsAsync((FoodChart)null);

            var result = await _controller.DeleteConfirmed(id) as NotFoundResult;

            Assert.NotNull(result);
        }
    }
}
