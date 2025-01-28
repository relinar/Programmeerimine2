using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
            // Arrange: Create mock data
            var foodCharts = new List<FoodChart>
            {
                new FoodChart { Id = 1, InvoiceNo = "INV001", InvoiceDate = DateTime.Now, user = "User1", date = "2025-01-28", meal = "Meal1", nutrients = DateTime.Now, amount = 200.5f },
                new FoodChart { Id = 2, InvoiceNo = "INV002", InvoiceDate = DateTime.Now, user = "User2", date = "2025-01-28", meal = "Meal2", nutrients = DateTime.Now, amount = 150.5f }
            };

            // Mock the service to return PagedResult<FoodChart>
            _foodChartServiceMock.Setup(service => service.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<FoodChartSearch>()))
                .ReturnsAsync(new PagedResult<FoodChart>(
                    currentPage: 1,
                    pageSize: 5,
                    rowCount: foodCharts.Count,
                    results: foodCharts
                ));

            // Act: Call the Index action
            var result = await _controller.Index(1) as ViewResult;

            // Assert: Check if the result is of type ViewResult and contains the expected model
            Assert.NotNull(result);
            var model = result.Model as FoodChartIndexModel; // Assuming you have this model
            Assert.NotNull(model);
            Assert.Equal(foodCharts, model.Data.Results); // Adjust this if necessary (model.Data.Results should hold the list of food charts)
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        // Testing the Details method
        [Fact]
        public async Task Details_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0;

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

        // Testing the Create method
        [Fact]
        public void Create_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        // Testing the Edit method
        [Fact]
        public async Task Edit_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0;

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

        // Testing the Delete method
        [Fact]
        public async Task Delete_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0;

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
    }
}
