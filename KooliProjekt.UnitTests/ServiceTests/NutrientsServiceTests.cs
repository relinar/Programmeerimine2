using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Search;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class NutrientsServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<INutrientsRepository> _repositoryMock;
        private readonly NutrientsService _nutrientsService;

        public NutrientsServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<INutrientsRepository>();
            _nutrientsService = new NutrientsService(_uowMock.Object);

            // Setup the mock to return the repository mock when the NutrientsRepository is accessed
            _uowMock.SetupGet(uow => uow.Nutrients).Returns(_repositoryMock.Object);
        }
       
        [Fact]
        public async Task Get_should_return_nutrient_for_valid_id()
        {
            // Arrange
            var nutrient = new Nutrients { Id = 1, Name = "Vitamin C", Carbohydrates = 10, Fats = 0, Sugars = 2 };
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(nutrient);

            // Act
            var result = await _nutrientsService.Get(1);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(1, result.Id);  // Ensure the correct data is returned
        }

        [Fact]
        public async Task List_should_return_paged_nutrients()
        {
            // Arrange
            var results = new List<Nutrients>
            {
                new Nutrients { Id = 1, Name = "Vitamin C", Carbohydrates = 10, Fats = 0, Sugars = 2 },
                new Nutrients { Id = 2, Name = "Iron", Carbohydrates = 5, Fats = 1, Sugars = 1 }
            };
            var pagedResult = new PagedResult<Nutrients> { Results = results, CurrentPage = 1, PageSize = 10, RowCount = 2, PageCount = 1 };
            _repositoryMock.Setup(repo => repo.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<NutrientsSearch>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _nutrientsService.List(1, 10, new NutrientsSearch());

            // Assert
            Assert.Equal(pagedResult, result);  // Ensure the result matches the expected paged result
        }
    }
}
