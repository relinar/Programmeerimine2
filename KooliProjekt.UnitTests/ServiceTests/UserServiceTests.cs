using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class UserServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_user()
        {
            // Arrange
            var service = new UserService(DbContext);
            var user = new User
            {
                Name = "John Doe",
                Role = "Admin",
                DailySummary = DateTime.Now,
                Meal = DateTime.Now
            };

            // Act
            await service.Save(user);

            // Assert
            var result = DbContext.User.FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Role, result.Role);
        }

        [Fact]
        public async Task Save_should_update_existing_user()
        {
            // Arrange
            var service = new UserService(DbContext);
            var user = new User
            {
                Name = "John Doe",
                Role = "Admin",
                DailySummary = DateTime.Now,
                Meal = DateTime.Now
            };
            DbContext.User.Add(user);
            await DbContext.SaveChangesAsync();

            // Update the user
            user.Name = "Jane Doe";

            // Act
            await service.Save(user);

            // Assert
            var updatedUser = DbContext.User.FirstOrDefault(u => u.Name == "Jane Doe");
            Assert.NotNull(updatedUser);
            Assert.Equal("Jane Doe", updatedUser.Name);
        }
 

        [Fact]
        public async Task Get_should_return_null_for_non_existing_user()
        {
            // Arrange
            var service = new UserService(DbContext);

            // Act
            var result = await service.Get(999); // Non-existing ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task List_should_return_users_for_given_page()
        {
            // Arrange
            var service = new UserService(DbContext);

            var user1 = new User
            {
                Name = "User1",
                Role = "Admin",
                DailySummary = DateTime.Now,
                Meal = DateTime.Now
            };
            var user2 = new User
            {
                Name = "User2",
                Role = "User",
                DailySummary = DateTime.Now,
                Meal = DateTime.Now
            };
            var user3 = new User
            {
                Name = "User3",
                Role = "Admin",
                DailySummary = DateTime.Now,
                Meal = DateTime.Now
            };

            DbContext.User.AddRange(user1, user2, user3);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.List(1, 2, null); // Page 1, size 2

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count()); // Ensure that the result only contains 2 users
            Assert.Contains(result.Results, u => u.Name == "User1");
            Assert.Contains(result.Results, u => u.Name == "User2");
        }

        [Fact]
        public async Task List_should_return_empty_for_empty_page()
        {
            // Arrange
            var service = new UserService(DbContext);

            // Clear the users for this test
            DbContext.User.RemoveRange(DbContext.User);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.List(100, 10, null); // Requesting a non-existent page

            // Assert
            Assert.Empty(result.Results); // Ensure no items are returned for an empty page
        }
    }
}
