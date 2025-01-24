using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class NutrientsControllerTests
    {
        private readonly NutrientsController _controller;
        private readonly Mock<INutrientsService> _nutrientsServiceMock;

        public NutrientsControllerTests()
        {
            // Initialize mock service and controller
            _nutrientsServiceMock = new Mock<INutrientsService>();
            _controller = new NutrientsController(_nutrientsServiceMock.Object);
        }

        // Test the Index action
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange: Prepare mock search data
            var search = new NutrientsSearch
            {
                Carbohydrates = "10",
                Fats = "3",
                Name = "Test Nutrient",
                Sugars = "5"
            };

            // Create PagedResult without 'pageCount' (calculated dynamically)
            var pagedResult = new PagedResult<Nutrients>(
                currentPage: 1,
                pageSize: 10,
                rowCount: 2,
                results: new List<Nutrients>
                {
                    new Nutrients { Id = 1, Name = "Protein" },
                    new Nutrients { Id = 2, Name = "Carbohydrates" }
                }
            );

            // Mock the service to return the prepared PagedResult
            _nutrientsServiceMock.Setup(service => service.List(1, 10, search))
                .ReturnsAsync(pagedResult);

            // Act: Call the Index action
            var result = await _controller.Index(1, search) as ViewResult;

            // Assert: Ensure that the view returns the correct model and data
            var model = result?.Model as NutrientsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model?.Data.RowCount);  // Ensure RowCount is correct
            Assert.Equal("Test Nutrient", model?.Search.Name);  // Ensure the search field is correct
            Assert.Equal("Protein", model?.Data.Results[0].Name);  // First nutrient name
        }

        // Test the Create action (GET)
        [Fact]
        public void Create_should_return_view()
        {
            // Act: Call the Create action (GET)
            var result = _controller.Create() as ViewResult;

            // Assert: Ensure the result is a ViewResult and the view is not null
            Assert.NotNull(result);
        }

        // Test the Create action (POST) with valid data
        [Fact]
        public async Task Create_should_redirect_to_index_when_nutrient_is_valid()
        {
            // Arrange: Create a new nutrient
            var nutrient = new Nutrients { Id = 1, Name = "Vitamin C" };

            // Mock the service to add the nutrient
            _nutrientsServiceMock.Setup(service => service.Add(nutrient)).Returns(Task.CompletedTask);

            // Act: Call the Create action (POST)
            var result = await _controller.Create(nutrient) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Test the Edit action (GET) - Returns NotFound if ID is invalid
        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_null()
        {
            // Arrange: Null ID
            int? id = null;

            // Act: Call the Edit action (GET)
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert: Ensure result is NotFound
            Assert.NotNull(result);
        }

        // Test the Edit action (POST) with valid data
        [Fact]
        public async Task Edit_should_redirect_to_index_when_nutrient_is_valid()
        {
            // Arrange: Create a nutrient to edit
            var nutrient = new Nutrients { Id = 1, Name = "Vitamin D" };

            // Mock the service to update the nutrient
            _nutrientsServiceMock.Setup(service => service.Update(nutrient)).Returns(Task.CompletedTask);

            // Act: Call the Edit action (POST)
            var result = await _controller.Edit(1, nutrient) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Test the Delete action (GET) - Returns NotFound if ID is invalid
        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_null()
        {
            // Arrange: Null ID
            int? id = null;

            // Act: Call the Delete action (GET)
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert: Ensure result is NotFound
            Assert.NotNull(result);
        }

        // Test the Delete action (POST) - Deletes nutrient and redirects to Index
        [Fact]
        public async Task DeleteConfirmed_should_redirect_to_index()
        {
            // Arrange: Nutrient ID
            int nutrientId = 1;

            // Mock the service to delete the nutrient
            _nutrientsServiceMock.Setup(service => service.Delete(nutrientId)).Returns(Task.CompletedTask);

            // Act: Call the DeleteConfirmed action (POST)
            var result = await _controller.DeleteConfirmed(nutrientId) as RedirectToActionResult;

            // Assert: Ensure the action redirects to the Index view
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // Test the Details action (GET)
        [Fact]
        public async Task Details_should_return_notfound_when_id_is_null()
        {
            // Arrange: Null ID
            int? id = null;

            // Act: Call the Details action (GET)
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert: Ensure result is NotFound
            Assert.NotNull(result);
        }

        // Test the Details action (GET) - Returns the correct nutrient details
        [Fact]
        public async Task Details_should_return_view_with_model_when_nutrient_found()
        {
            // Arrange: Nutrient ID
            int id = 1;
            var nutrient = new Nutrients { Id = id, Name = "Vitamin A" };

            // Mock the service to return a nutrient
            _nutrientsServiceMock.Setup(service => service.Get(id)).ReturnsAsync(nutrient);

            // Act: Call the Details action (GET)
            var result = await _controller.Details(id) as ViewResult;

            // Assert: Ensure the result is a ViewResult and the nutrient is returned
            Assert.NotNull(result);
            Assert.Equal(nutrient, result.Model);
        }
    }
}
