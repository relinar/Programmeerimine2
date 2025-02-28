using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Data;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class AmountsControllerTests
    {
        private readonly Mock<IAmountService> _amountServiceMock;
        private readonly AmountsController _controller;

        public AmountsControllerTests()
        {
            _amountServiceMock = new Mock<IAmountService>();
            _controller = new AmountsController(_amountServiceMock.Object);
        }
        [Fact]
        public async Task Details_ReturnsNotFound_WhenAmountIsNull()
        {
            // Arrange
            var mockAmountService = new Mock<IAmountService>();
            mockAmountService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync((Amount)null);

            var controller = new AmountsController(mockAmountService.Object);

            // Act
            var result = await controller.Details(1);  // Passing an ID for the test

            // Assert
            Assert.IsType<NotFoundResult>(result);  // Expecting NotFound if the amount doesn't exist
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenAmountIsFound()
        {
            // Arrange
            var mockAmountService = new Mock<IAmountService>();
            var amount = new Amount { AmountID = 1, NutrientsID = 101, AmountDate = DateTime.Now };  // Sample Amount object
            mockAmountService.Setup(service => service.Get(It.IsAny<int>())).ReturnsAsync(amount);

            var controller = new AmountsController(mockAmountService.Object);

            // Act
            var result = await controller.Details(1);  // Passing a valid ID for the test

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Amount>(viewResult.Model);  // Ensure the model is of type Amount
            Assert.Equal(1, model.AmountID);  // Verify that the returned model has the correct AmountID
        }
        [Fact]
        public async Task Index_Should_Return_View_With_Amounts()
        {
            // Arrange: Create mock data
            var amounts = new List<Amount>
    {
        new Amount { AmountID = 1, NutrientsID = 1, AmountDate = DateTime.Now },
        new Amount { AmountID = 2, NutrientsID = 2, AmountDate = DateTime.Now }
    };

            // Mock the service to return a PagedResult of amounts
            var pagedResult = new PagedResult<Amount>(
                currentPage: 1,
                pageSize: 10,
                rowCount: amounts.Count,
                results: amounts
            );

            _amountServiceMock.Setup(service => service.List(1, It.IsAny<int>(), It.IsAny<amountSearch>()))
                .ReturnsAsync(pagedResult);

            // Act: Call the Index action
            var result = await _controller.Index(1) as ViewResult;

            // Assert: Ensure the result is not null
            Assert.NotNull(result);

            // Check if the model is not null
            Assert.NotNull(result.Model);

            // Check if the model is of the correct type (PagedResult<Amount>)
            Assert.IsType<PagedResult<Amount>>(result.Model);

            // Check the model contents if valid
            var model = result.Model as PagedResult<Amount>;
            Assert.Equal(amounts.Count, model.Results.Count);  // Assert the list count
            Assert.Equal(amounts[0].AmountID, model.Results[0].AmountID);  // Assert first item match
            Assert.Equal(amounts[1].AmountID, model.Results[1].AmountID);  // Assert second item match

            // Ensure the correct view is returned
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }



        [Fact]
        public async Task Edit_Should_Return_View_When_ModelState_Is_Invalid()
        {
            var amount = new Amount { AmountID = 1 };
            _controller.ModelState.AddModelError("AmountID", "Required");
            var result = await _controller.Edit(amount) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(amount, result?.Model);
        }

        [Fact]
        public async Task Edit_Should_Redirect_To_Index_When_Valid()
        {
            var amount = new Amount { AmountID = 1 };
            _amountServiceMock.Setup(service => service.UpdateAmountAsync(It.IsAny<Amount>())).Returns(Task.CompletedTask);
            var result = await _controller.Edit(amount) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
        [Fact]
        public async Task Edit_Get_Should_Return_NotFound_When_Amount_Is_Missing()
        {
            int id = 1;
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Amount)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Get_Should_Return_View_With_Model_When_Amount_Was_Found()
        {
            int id = 1;
            var amount = new Amount { AmountID = id };
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync(amount);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(amount, result.Model);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0;

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_Amount_Is_Missing()
        {
            int id = 1;
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Amount)null);

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_Should_Return_View_With_Model_When_Amount_Was_Found()
        {
            int id = 1;
            var amount = new Amount { AmountID = id, NutrientsID = 1, AmountDate = DateTime.Now };
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync(amount);

            var result = await _controller.Details(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(amount, result.Model);
        }

        [Fact]
        public void Create_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Should_Return_View_When_ModelState_Is_Invalid()
        {
            _controller.ModelState.AddModelError("AmountID", "Required");
            var amount = new Amount();
            var result = await _controller.Create(amount) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(amount, result?.Model);
        }

        [Fact]
        public async Task Create_Should_Redirect_To_Index_When_Valid()
        {
            var amount = new Amount { AmountID = 1 };
            _amountServiceMock.Setup(service => service.AddAmountAsync(It.IsAny<Amount>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(amount) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


        [Fact]
        public async Task Delete_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_Amount_Is_Missing()
        {
            int id = 1;
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Amount)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Should_Return_View_With_Model_When_Amount_Was_Found()
        {
            int id = 1;
            var amount = new Amount { AmountID = id };
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync(amount);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(amount, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Redirect_To_Index_When_Valid()
        {
            int id = 1;
            _amountServiceMock.Setup(service => service.Delete(id)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

       
        }
    }
}
