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
    public class FoodChartsControllerTests
    {
        private readonly Mock<IFoodChartService> _foodChartServiceMock;
        private readonly FoodChartsController _controller;

        public FoodChartsControllerTests()
        {
            _foodChartServiceMock = new Mock<IFoodChartService>();
            _controller = new FoodChartsController(_foodChartServiceMock.Object);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0; // Assuming 0 is an invalid ID

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

        [Fact]
        public void Create_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

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
