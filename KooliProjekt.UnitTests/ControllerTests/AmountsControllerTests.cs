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
        public async Task Edit_Should_Return_NotFound_When_ID_Is_Missing()
        {
            int id = 0; // Assuming 0 is an invalid ID

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_When_Amount_Is_Missing()
        {
            int id = 1;
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Amount)null);

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Should_Return_View_With_Model_When_Amount_Was_Found()
        {
            int id = 1;
            var amount = new Amount { AmountID = id };
            _amountServiceMock.Setup(x => x.Get(id)).ReturnsAsync(amount);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
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
            var amount = new Amount { AmountID = id };
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
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(amount, result.Model);
        }
    }
}
