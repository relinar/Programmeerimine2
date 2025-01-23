// File: UnitTests/ControllerTests/HealthDatasControllerTests.cs
using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            var searchModel = new HealthDataSearch
            {
                User = "Test User",
                BloodSugar = 120
            };

            var healthDataList = new List<HealthData>
            {
                new HealthData { User = "Test User", BloodSugar = 120 },
                new HealthData { User = "Test User 2", BloodSugar = 100 }
            };

            var pagedResult = new PagedResult<HealthData>
            {
                Results = healthDataList,
                CurrentPage = 1,
                PageCount = 1,
                RowCount = 2
            };

            _healthDataServiceMock.Setup(x => x.List(1, 5, searchModel)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(1, searchModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}
