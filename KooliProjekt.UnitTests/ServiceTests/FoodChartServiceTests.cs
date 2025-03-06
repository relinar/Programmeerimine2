using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class FoodChartServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_food_chart()
        {
            // Arrange
            var service = new FoodChartService(DbContext);
            var foodChart = new FoodChart
            {
                InvoiceNo = "INV001",
                user = "TestUser",
                meal = "Breakfast",
                InvoiceDate = DateTime.Now
            };

            // Act
            await service.Save(foodChart);

            // Assert
            var count = DbContext.food_Chart.Count();
            var result = DbContext.food_Chart.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(foodChart.InvoiceNo, result.InvoiceNo);
            Assert.Equal(foodChart.user, result.user);
            Assert.Equal(foodChart.meal, result.meal);
        }

        [Fact]
        public async Task Save_should_update_existing_food_chart()
        {
            // Arrange
            var service = new FoodChartService(DbContext);
            var foodChart = new FoodChart
            {
                Id = 1,
                InvoiceNo = "INV001",
                user = "TestUser",
                meal = "Lunch",
                InvoiceDate = DateTime.Now
            };

            DbContext.food_Chart.Add(foodChart);
            await DbContext.SaveChangesAsync();

            // Update the food chart
            foodChart.meal = "Dinner";

            // Act
            await service.Save(foodChart);

            // Assert
            var updatedFoodChart = DbContext.food_Chart.FirstOrDefault(f => f.Id == 1);
            Assert.NotNull(updatedFoodChart);
            Assert.Equal("Dinner", updatedFoodChart.meal);
        }

        [Fact]
        public async Task Delete_should_remove_given_food_chart()
        {
            // Arrange
            var service = new FoodChartService(DbContext);
            var foodChart = new FoodChart
            {
                InvoiceNo = "INV001",
                user = "TestUser",
                meal = "Breakfast",
                InvoiceDate = DateTime.Now
            };

            DbContext.food_Chart.Add(foodChart);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(foodChart.Id);

            // Assert
            var count = DbContext.food_Chart.Count();
            Assert.Equal(0, count); // Ensure it is deleted
        }
        [Fact]
        public async Task Get_ShouldReturnCorrectFoodChart_WhenExists()
        {
            // Arrange
            var service = new FoodChartService(DbContext);
            var foodChart = new FoodChart
            {
                InvoiceNo = "INV001",
                user = "TestUser",
                meal = "Breakfast",
                InvoiceDate = DateTime.Now
            };
            DbContext.food_Chart.Add(foodChart);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(foodChart.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(foodChart.Id, result.Id);  // Assert the correct food chart is returned
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenFoodChartDoesNotExist()
        {
            // Arrange
            var service = new FoodChartService(DbContext);

            // Act
            var result = await service.Get(999);  // Try fetching a non-existing food chart

            // Assert
            Assert.Null(result);  // Should return null for a non-existing food chart
        }

        [Fact]
        public async Task List_ShouldReturnPagedResults_WhenDataExists()
        {
            // Arrange
            var service = new FoodChartService(DbContext);

            // Seed multiple food charts
            for (int i = 0; i < 25; i++)
            {
                DbContext.food_Chart.Add(new FoodChart
                {
                    InvoiceNo = $"INV00{i}",
                    user = "TestUser",
                    meal = "Breakfast",
                    InvoiceDate = DateTime.Now
                });
            }
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.List(1, 10, null);  // Request the first page with 10 items per page

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Results.Count);  // Ensure 10 results on the first page
            Assert.Equal(3, result.PageCount);  // Total of 3 pages for 25 items
        }

        [Fact]
        public async Task List_ShouldReturnEmpty_WhenNoFoodChartsExist()
        {
            // Arrange
            var service = new FoodChartService(DbContext);

            // Act
            var result = await service.List(1, 10, null);  // Request the first page with 10 items per page

            // Assert
            Assert.NotNull(result);  // Ensure that the result is not null
            Assert.Empty(result.Results);  // No results should be returned as the table is empty
            Assert.Equal(0, result.RowCount);  // RowCount should be 0 since no food charts exist
            Assert.Equal(0, result.PageCount);  // PageCount should be 1, because PageCount will always be >= 1 even when RowCount is 0
        }



        [Fact]
        public async Task Delete_should_not_throw_exception_for_non_existing_food_chart()
        {
            // Arrange
            var service = new FoodChartService(DbContext);

            // Act: Try to delete a food chart with a non-existing ID (e.g. 999)
            await service.Delete(999);

            // Assert: Ensure no exception was thrown, and the operation completed successfully
            var foodChart = await DbContext.food_Chart.FindAsync(999);
            Assert.Null(foodChart);  // Ensure the food chart still doesn't exist
        }

        [Fact]
        public async Task Get_should_return_null_for_non_existing_food_chart()
        {
            // Arrange
            var service = new FoodChartService(DbContext);

            // Act
            var result = await service.Get(999); // Non-existing ID

            // Assert
            Assert.Null(result);
        }
    }
}
