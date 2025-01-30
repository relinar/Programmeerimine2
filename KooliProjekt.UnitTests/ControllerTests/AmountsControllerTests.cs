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
        public async Task Index_Should_Return_View_With_Amounts()
        {
            // Arrange: Create some mock data
            var amounts = new List<Amount>
            {
                new Amount { AmountID = 1, NutrientsID = 1, AmountDate = DateTime.Now },  // Correct property names here
                new Amount { AmountID = 2, NutrientsID = 2, AmountDate = DateTime.Now }   // Same here
            };

            // Mock the service to return the list of amounts
            _amountServiceMock.Setup(service => service.GetAmountsAsync()).ReturnsAsync(amounts);

            // Act: Call the Index action
            var result = await _controller.Index() as ViewResult;

            // Assert: Check if the result is of type ViewResult and contains the expected model
            Assert.NotNull(result);
            Assert.Equal(amounts, result.Model);

            // Ensure that the correct view is returned (null means default view, which is "Index")
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
