using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Search;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class HealthDataServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IHealthDataRepository> _repositoryMock;
        private readonly HealthDataService _healthDataService;

        public HealthDataServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IHealthDataRepository>();
            _healthDataService = new HealthDataService(_uowMock.Object);

            // Setup the mock to return the repository mock when the HealthDataRepository is accessed
            _uowMock.SetupGet(uow => uow.HealthDataRepository).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task Create_should_add_new_health_data()
        {
            // Arrange
            var healthData = new HealthData { User = "Test User", BloodSugar = 120 };

            // Mock the repository methods
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<HealthData>())).Returns(Task.CompletedTask);
            _uowMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);  // Mock CommitAsync

            // Act
            await _healthDataService.AddAsync(healthData);  // Call AddAsync
            await _healthDataService.SaveAsync();  // Call SaveAsync to trigger CommitAsync

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(healthData), Times.Once);  // Ensure AddAsync was called
            _uowMock.Verify(uow => uow.CommitAsync(), Times.Once);  // Ensure CommitAsync was called once after AddAsync
        }

        [Fact]
        public async Task Delete_should_remove_health_data()
        {
            // Arrange
            var healthData = new HealthData { Id = 1, User = "Test User", BloodSugar = 120 };

            // Mock the repository methods
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(healthData);
            _repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<HealthData>())).Returns(Task.CompletedTask);
            _uowMock.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);  // Mock CommitAsync

            // Act
            await _healthDataService.DeleteAsync(1);  // Call DeleteAsync
            await _healthDataService.SaveAsync();  // Call SaveAsync to trigger CommitAsync

            // Assert
            _repositoryMock.Verify(repo => repo.DeleteAsync(healthData), Times.Once);  // Ensure DeleteAsync was called
            _uowMock.Verify(uow => uow.CommitAsync(), Times.Once);  // Ensure CommitAsync was called once after DeleteAsync
        }

        // Test for fetching health data by ID (Get method)
        [Fact]
        public async Task Get_should_return_health_data_for_valid_id()
        {
            // Arrange
            var healthData = new HealthData { Id = 1, User = "Test User", BloodSugar = 120 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(healthData);

            // Act
            var result = await _healthDataService.Get(1);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(1, result.Id);  // Ensure the correct data is returned
        }

        // Test for listing health data with pagination (List method)
        [Fact]
        public async Task List_should_return_paged_health_data()
        {
            // Arrange
            var results = new List<HealthData>
            {
                new HealthData { Id = 1, User = "User 1", BloodSugar = 100 },
                new HealthData { Id = 2, User = "User 2", BloodSugar = 110 }
            };
            var pagedResult = new PagedResult<HealthData> { Results = results };
            _repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<HealthDataSearch>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _healthDataService.List(1, 10, new HealthDataSearch());

            // Assert
            Assert.Equal(pagedResult, result);  // Ensure the result matches the expected paged result
        }

    }
}
