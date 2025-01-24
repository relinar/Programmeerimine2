using KooliProjekt.Controllers;
using KooliProjekt.Models;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class FoodChartsControllerTests
    {
        private readonly Mock<IFoodChartService> _foodChartServiceMock;
        private readonly FoodChartsController _controller;

        public FoodChartsControllerTests()
        {
            // Mocki teenuse klass
            _foodChartServiceMock = new Mock<IFoodChartService>();
            _controller = new FoodChartsController(_foodChartServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange: Testiandmete ettevalmistamine
            int page = 1;
            var searchModel = new FoodChartSearch
            {
                InvoiceNo = "INV001",  // Testimisel otsime selle invoiceNumbriga toidugraafikuid
                user = "testuser",
                date = "2023-01-01",
                meal = "lunch"
            };

            var foodChartsData = new List<FoodChart>
            {
                new FoodChart
                {
                    Id = 1,
                    InvoiceNo = "INV001",
                    InvoiceDate = new DateTime(2023, 01, 01),
                    user = "testuser",
                    date = "2023-01-01",
                    meal = "lunch",
                    nutrients = new DateTime(2023, 01, 01),
                    amount = 50f
                },
                new FoodChart
                {
                    Id = 2,
                    InvoiceNo = "INV002",
                    InvoiceDate = new DateTime(2023, 01, 02),
                    user = "testuser",
                    date = "2023-01-02",
                    meal = "dinner",
                    nutrients = new DateTime(2023, 01, 02),
                    amount = 100f
                }
            };

            var pagedResult = new PagedResult<FoodChart>
            {
                Results = foodChartsData,
                CurrentPage = page,
                PageCount = 1, // Üks leht
                RowCount = foodChartsData.Count // Andmete arv
            };

            var indexModel = new FoodChartIndexModel
            {
                Data = pagedResult,
                Search = searchModel
            };

            // Konfigureeri mock, et see tagastaks õiged andmed
            _foodChartServiceMock.Setup(x => x.List(page, 5, searchModel)).ReturnsAsync(pagedResult);

            // Act: kutsume controlleri meetodi
            var result = await _controller.Index(page, indexModel) as ViewResult;

            // Assert: Veenduge, et vastus oleks korrektne
            Assert.NotNull(result); // Veenduge, et vastus pole tühi
            Assert.Equal(indexModel, result.Model); // Veenduge, et tagastatud mudel oleks õige
        }
    }
}
