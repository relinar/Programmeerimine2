// Unit Test: NutrientsControllerTests.cs
using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using KooliProjekt.Data;


namespace KooliProjekt.UnitTests.ControllerTests
{ 

 public class NutrientsControllerTests
 {
    private readonly Mock<INutrientsService> _nutrientsServiceMock;
    private readonly NutrientsController _controller;

    public NutrientsControllerTests()
    {
        // Mock the INutrientsService dependency
        _nutrientsServiceMock = new Mock<INutrientsService>();

        // Initialize the controller with the mocked service
        _controller = new NutrientsController(_nutrientsServiceMock.Object);
    }

    [Fact]
    public async Task Index_should_return_correct_view_with_data()
    {
        // Arrange
        var search = new NutrientsSearch
        {
            Carbohydrates = "10",
            Fats = "3",
            Name = "Test Nutrient",
            Sugars = "5"
        };

        var pagedResult = new PagedResult<Nutrients>
        {
            CurrentPage = 1,
            PageCount = 1,
            PageSize = 10,
            RowCount = 2,
            Results = new List<Nutrients>
            {
                new Nutrients { Id = 1, Name = "Protein" },
                new Nutrients { Id = 2, Name = "Carbohydrates" }
            }
        };

        _nutrientsServiceMock.Setup(service => service.List(1, 10, search))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.Index(1) as ViewResult;

        // Assert
        var model = result?.Model as NutrientsIndexModel;
        Assert.NotNull(model);
        Assert.Equal(2, model?.Data.RowCount); // Check that RowCount is correct
        Assert.Equal(search.Name, model?.Search.Name); // Check that search model is correct
        Assert.Equal("Protein", model?.Data.Results[0].Name); // Check first result
    }
 }
}