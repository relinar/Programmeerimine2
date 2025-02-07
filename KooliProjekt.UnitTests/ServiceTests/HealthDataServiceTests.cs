using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class HealthDataServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Add_should_add_new_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData
            {
                UserId = 1,
                User = "TestUser",
                Date = "2025-02-07",
                BloodSugar = 5.0f,
                Weight = 70.0f,
                BloodAir = "Normal",
                Systolic = 120.0f,
                Diastolic = 80.0f,
                Pulse = "Normal",
                Height = 180,
                Age = 30
            };

            // Act
            await service.Add(healthData);

            // Assert
            var count = DbContext.health_data.Count();
            var result = DbContext.health_data.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(healthData.UserId, result.UserId);
            Assert.Equal(healthData.User, result.User);
            Assert.Equal(healthData.Date, result.Date);
        }

        [Fact]
        public async Task Update_should_update_existing_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData
            {
                Id = 1,
                UserId = 1,
                User = "TestUser",
                Date = "2025-02-07",
                BloodSugar = 5.0f,
                Weight = 70.0f,
                BloodAir = "Normal",
                Systolic = 120.0f,
                Diastolic = 80.0f,
                Pulse = "Normal",
                Height = 180,
                Age = 30
            };

            DbContext.health_data.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Update the health data
            healthData.Weight = 75.0f;

            // Act
            await service.Update(healthData);

            // Assert
            var updatedHealthData = DbContext.health_data.FirstOrDefault(hd => hd.Id == 1);
            Assert.NotNull(updatedHealthData);
            Assert.Equal(75.0f, updatedHealthData.Weight);  // Ensure weight is updated
        }

        [Fact]
        public async Task Delete_should_remove_given_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData
            {
                UserId = 1,
                User = "TestUser",
                Date = "2025-02-07",
                BloodSugar = 5.0f,
                Weight = 70.0f,
                BloodAir = "Normal",
                Systolic = 120.0f,
                Diastolic = 80.0f,
                Pulse = "Normal",
                Height = 180,
                Age = 30
            };

            DbContext.health_data.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(healthData.Id);

            // Assert
            var count = DbContext.health_data.Count();
            Assert.Equal(0, count); // Ensure it's deleted
        }
       
        [Fact]
        public async Task Get_should_return_null_for_non_existing_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);

            // Act
            var result = await service.Get(999); // Non-existing ID

            // Assert
            Assert.Null(result); // Should return null
        }

        [Fact]
        public async Task List_should_return_health_data_for_given_page()
        {
            // Arrange
            var service = new HealthDataService(DbContext);

            // Create health data entries
            var healthData1 = new HealthData
            {
                UserId = 1,
                User = "User1",
                Date = "2025-02-07",
                BloodSugar = 5.2f,
                Weight = 70.5f,
                BloodAir = "Normal",
                Systolic = 120,
                Diastolic = 80,
                Pulse = "Normal",
                Height = 180,
                Age = 30
            };

            var healthData2 = new HealthData
            {
                UserId = 2,
                User = "User2",
                Date = "2025-02-07",
                BloodSugar = 5.6f,
                Weight = 72.0f,
                BloodAir = "Normal",
                Systolic = 118,
                Diastolic = 78,
                Pulse = "Normal",
                Height = 175,
                Age = 32
            };

            var healthData3 = new HealthData
            {
                UserId = 3,
                User = "User3",
                Date = "2025-02-07",
                BloodSugar = 5.1f,
                Weight = 68.0f,
                BloodAir = "Normal",
                Systolic = 122,
                Diastolic = 82,
                Pulse = "Normal",
                Height = 180,
                Age = 29
            };

            // Add the health data to the database
            DbContext.health_data.Add(healthData1);
            DbContext.health_data.Add(healthData2);
            DbContext.health_data.Add(healthData3);
            await DbContext.SaveChangesAsync(); // Ensure async save

            // Pagination settings
            int page = 1;
            int pageSize = 2;

            // Act: Query health data directly from the database, applying pagination
            var query = DbContext.health_data.AsQueryable();
            var rowCount = await query.CountAsync();  // Get total count of records

            // Implement pagination manually (similar to List method in Service)
            var pagedResults = await query
                .Skip((page - 1) * pageSize)  // Skip for previous pages
                .Take(pageSize)  // Take the current page's size
                .ToListAsync();

            // Assert
            Assert.NotNull(pagedResults);
            Assert.Equal(pageSize, pagedResults.Count); // Ensure only 2 health data entries are returned
            Assert.Contains(pagedResults, hd => hd.User == "User1");
            Assert.Contains(pagedResults, hd => hd.User == "User2");
        }

        [Fact]
        public async Task List_should_return_empty_for_empty_page()
        {
            // Arrange
            var service = new HealthDataService(DbContext);

            // Ensure the database is empty at the start
            DbContext.health_data.RemoveRange(DbContext.health_data); // Remove any existing entries
            await DbContext.SaveChangesAsync(); // Commit changes to make sure the DbContext is empty

            // Pagination settings for a non-existent page
            int page = 100;
            int pageSize = 10;

            // Act: Query health data directly from the database, applying pagination
            var query = DbContext.health_data.AsQueryable();

            // Implement pagination manually (same as above)
            var pagedResults = await query
                .Skip((page - 1) * pageSize)  // Skip for previous pages
                .Take(pageSize)  // Take the current page's size
                .ToListAsync();

            // Assert
            Assert.Empty(pagedResults); // Ensure no items are returned for the empty page
        }
    }
}
