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
    public class NutrientsServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Add_should_add_new_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);
            var nutrient = new Nutrients
            {
                Name = "Vitamin C",
                Carbohydrates = 0.0f,
                Sugars = 0.0f,
                Fats = 0.0f,
                FoodChart = DateTime.Now
            };

            // Act
            await service.Add(nutrient);

            // Assert
            var count = DbContext.nutrients.Count();
            var result = DbContext.nutrients.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(nutrient.Name, result.Name);
            Assert.Equal(nutrient.Carbohydrates, result.Carbohydrates);
            Assert.Equal(nutrient.Sugars, result.Sugars);
            Assert.Equal(nutrient.Fats, result.Fats);
        }

        [Fact]
        public async Task Update_should_update_existing_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);
            var nutrient = new Nutrients
            {
                Id = 1,
                Name = "Vitamin A",
                Carbohydrates = 0.0f,
                Sugars = 0.0f,
                Fats = 0.0f,
                FoodChart = DateTime.Now
            };
            DbContext.nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Update the nutrient
            nutrient.Name = "Vitamin D";
            nutrient.Carbohydrates = 0.1f;

            // Act
            await service.Update(nutrient);

            // Assert
            var updatedNutrient = DbContext.nutrients.FirstOrDefault(n => n.Id == 1);
            Assert.NotNull(updatedNutrient);
            Assert.Equal("Vitamin D", updatedNutrient.Name);
            Assert.Equal(0.1f, updatedNutrient.Carbohydrates);
        }

        [Fact]
        public async Task Delete_should_remove_given_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);
            var nutrient = new Nutrients
            {
                Name = "Vitamin C",
                Carbohydrates = 0.0f,
                Sugars = 0.0f,
                Fats = 0.0f,
                FoodChart = DateTime.Now
            };
            DbContext.nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(nutrient.Id);

            // Assert
            var count = DbContext.nutrients.Count();
            Assert.Equal(0, count); // Ensure it's deleted
        }


        [Fact]
        public async Task Get_should_return_null_for_non_existing_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            // Act
            var result = await service.Get(999); // Non-existing ID

            // Assert
            Assert.Null(result);
        }
       

        [Fact]
        public async Task List_should_return_filtered_nutrients_by_name()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            var nutrient1 = new Nutrients
            {
                Name = "Vitamin A",
                Carbohydrates = 0.1f,
                Sugars = 0.0f,
                Fats = 0.1f,
                FoodChart = DateTime.Now
            };

            var nutrient2 = new Nutrients
            {
                Name = "Vitamin B",
                Carbohydrates = 0.5f,
                Sugars = 0.2f,
                Fats = 0.3f,
                FoodChart = DateTime.Now
            };

            DbContext.nutrients.Add(nutrient1);
            DbContext.nutrients.Add(nutrient2);
            await DbContext.SaveChangesAsync();

            // Act
            var search = new NutrientsSearch { Name = "Vitamin A" }; // Search by nutrient name
            var result = await service.List(1, 10, search); // Page 1 with filter

            // Assert
            Assert.Single(result.Results);  // Should return one result
            Assert.Contains(result.Results, n => n.Name == "Vitamin A");
        }

        [Fact]
        public async Task List_should_return_empty_if_no_matching_results()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            var nutrient1 = new Nutrients
            {
                Name = "Vitamin A",
                Carbohydrates = 0.1f,
                Sugars = 0.0f,
                Fats = 0.1f,
                FoodChart = DateTime.Now
            };

            var nutrient2 = new Nutrients
            {
                Name = "Vitamin B",
                Carbohydrates = 0.5f,
                Sugars = 0.2f,
                Fats = 0.3f,
                FoodChart = DateTime.Now
            };

            DbContext.nutrients.Add(nutrient1);
            DbContext.nutrients.Add(nutrient2);
            await DbContext.SaveChangesAsync();

            // Act
            var search = new NutrientsSearch { Name = "Nonexistent Vitamin" }; // Search for a name that doesn't exist
            var result = await service.List(1, 10, search); // Page 1 with filter

            // Assert
            Assert.Empty(result.Results);  // Should return no results
        }

       

        [Fact]
        public async Task List_should_return_empty_when_no_nutrients_match_filter()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            // Act - Search for a non-existent nutrient by Name
            var search = new NutrientsSearch { Name = "NonExistent" };
            var result = await service.List(1, 10, search); // Page 1 with filter

            // Assert
            Assert.Empty(result.Results); // No nutrients should match
        }
        [Fact]
        public async Task Update_should_modify_existing_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            var nutrient = new Nutrients
            {
                Name = "Vitamin A",
                Carbohydrates = 0.1f,
                Sugars = 0.0f,
                Fats = 0.1f,
                FoodChart = DateTime.Now
            };
            DbContext.nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act - Modify the nutrient
            nutrient.Fats = 0.2f; // Update Fats value
            await service.Update(nutrient);

            // Assert
            var updatedNutrient = await DbContext.nutrients.FirstOrDefaultAsync(n => n.Id == nutrient.Id);
            Assert.NotNull(updatedNutrient);
            Assert.Equal(0.2f, updatedNutrient.Fats);
        }

       

        [Fact]
        public async Task Delete_should_remove_existing_nutrient()
        {
            // Arrange
            var service = new NutrientsService(DbContext);

            var nutrient = new Nutrients
            {
                Name = "Vitamin A",
                Carbohydrates = 0.1f,
                Sugars = 0.0f,
                Fats = 0.1f,
                FoodChart = DateTime.Now
            };
            DbContext.nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(nutrient.Id);

            // Assert
            var deletedNutrient = await DbContext.nutrients.FirstOrDefaultAsync(n => n.Id == nutrient.Id);
            Assert.Null(deletedNutrient);  // Ensure the nutrient is deleted
        }
        [Fact]
        public async Task Get_should_return_nutrient_by_id()
        {
            // Arrange
            var service = new NutrientsService(DbContext);
            var nutrient = new Nutrients
            {
                Name = "Vitamin A",
                Carbohydrates = 0.1f,
                Sugars = 0.0f,
                Fats = 0.1f,
                FoodChart = DateTime.Now
            };
            DbContext.nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(nutrient.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nutrient.Name, result.Name);
        }

     
    }
}
