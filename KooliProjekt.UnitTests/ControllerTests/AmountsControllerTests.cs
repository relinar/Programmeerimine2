using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Data;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Services;

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
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1; // Lehekülg, mille andmed taotleme
            var searchModel = new amountSearch
            {
                AmountID = 1,
                NutrientsID = 1,
                AmountDate = new System.DateTime(2023, 01, 01)
            };

            var amountsData = new List<Amount>
            {
                new Amount { AmountID = 1, NutrientsID = 1, AmountDate = new System.DateTime(2023, 01, 01) },
                new Amount { AmountID = 2, NutrientsID = 2, AmountDate = new System.DateTime(2023, 01, 02) }
            };

            var pagedResult = new PagedResult<Amount>
            {
                Results = amountsData,
                CurrentPage = page,
                PageCount = 1,
                RowCount = amountsData.Count // Kasutame RowCount'i, et määrata kirje kogusumma
            };

            var indexModel = new AmountIndexModel
            {
                Data = pagedResult,
                Search = searchModel
            };

            _amountServiceMock.Setup(x => x.List(page, 5, searchModel)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page, indexModel) as ViewResult;

            // Assert
            Assert.NotNull(result); // Veenduge, et vastus ei ole tühi
            Assert.Equal(indexModel, result.Model); // Veenduge, et tagastatud mudel oleks õige
        }
    }
}
