using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Search;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class FoodChartServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IFoodChartRepository> _repositoryMock;
        private readonly FoodChartService _foodChartService;

        public FoodChartServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IFoodChartRepository>();
            _foodChartService = new FoodChartService(_uowMock.Object);

            // Setup the mock to return the repository mock when the FoodChartRepository is accessed
            _uowMock.SetupGet(uow => uow.FoodCharts).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task Get_should_return_food_chart_for_valid_id()
        {
            // Arrange
            var foodChart = new FoodChart { Id = 1, InvoiceNo = "INV001", user = "TestUser" };
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(foodChart);

            // Act
            var result = await _foodChartService.Get(1);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(1, result.Id);  // Ensure the correct data is returned
        }
        
        [Fact]
        public async Task List_should_return_paged_food_charts()
        {
            // Arrange
            var results = new List<FoodChart>
            {
                new FoodChart { Id = 1, InvoiceNo = "INV001", user = "User 1", meal = "Breakfast" },
                new FoodChart { Id = 2, InvoiceNo = "INV002", user = "User 2", meal = "Dinner" }
            };
            var pagedResult = new PagedResult<FoodChart> { Results = results };
            _repositoryMock.Setup(repo => repo.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<FoodChartSearch>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _foodChartService.List(1, 10, new FoodChartSearch());

            // Assert
            Assert.Equal(pagedResult, result);  // Ensure the result matches the expected paged result
        }
    }
}
