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
using KooliProjekt.Search;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange: Mock data for the search and user list 
            var userSearch = new UserSearch
            {
                Name = "Test User",
                Role = "Admin"
            };

            var userIndexModel = new UserIndexModel
            {
                Search = userSearch // Assign UserSearch to UserIndexModel 
            };

            var userList = new List<User>
            {
                new User { Id = 1, Name = "Test User", Role = "Admin", DailySummary = DateTime.Now, Meal = DateTime.Now },
                new User { Id = 2, Name = "Test User 2", Role = "User", DailySummary = DateTime.Now, Meal = DateTime.Now }
            };

            // Create PagedResult correctly without 'pageCount' (since it's calculated dynamically)
            var pagedResult = new PagedResult<User>(1, 5, 2, userList); // (currentPage, pageSize, rowCount, results)

            // Setup the service mock to return the paged result 
            _userServiceMock.Setup(x => x.List(1, 5, userSearch)).ReturnsAsync(pagedResult);

            // Act: Call the Index action with the UserIndexModel 
            var result = await _controller.Index(1, userIndexModel) as ViewResult;

            // Assert 
            var model = result?.Model as UserIndexModel; // Ensure that the result is of the expected type 
            Assert.NotNull(model); // Check that the model is not null 
            Assert.Equal(pagedResult, model?.Data); // Check that the Data property of the UserIndexModel is correct 
            Assert.Equal(userSearch, model?.Search); // Check that the Search property is correct 
        }

        // Edit: If ID is missing, return NotFound 
        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange 
            int? id = null;

            // Act 
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Edit: If User not found, return NotFound 
        [Fact]
        public async Task Edit_should_return_notfound_when_user_is_missing()
        {
            // Arrange 
            int id = 1;
            var user = (User)null;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Edit: If User found, return View with model 
        [Fact]
        public async Task Edit_should_return_view_with_model_when_user_was_found()
        {
            // Arrange 
            int id = 1;
            var user = new User { Id = id };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Edit(id) as ViewResult;

            // Assert 
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit"
            );
            Assert.Equal(user, result.Model);
        }

        // Details: If ID is missing, return NotFound 
        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange 
            int? id = null;

            // Act 
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Details: If User not found, return NotFound 
        [Fact]
        public async Task Details_should_return_notfound_when_user_is_missing()
        {
            // Arrange 
            int id = 1;
            var user = (User)null;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Details: If User found, return View with model 
        [Fact]
        public async Task Details_should_return_view_with_model_when_user_was_found()
        {
            // Arrange 
            int id = 1;
            var user = new User { Id = id };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Details(id) as ViewResult;

            // Assert 
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details"
            );
            Assert.Equal(user, result.Model);
        }

        // Create (GET): Return View without model 
        [Fact]
        public void Create_should_return_view()
        {
            // Act 
            var result = _controller.Create() as ViewResult;

            // Assert 
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create"
            );
        }

        // Delete (GET): If ID is missing, return NotFound 
        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            // Arrange 
            int? id = null;

            // Act 
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Delete (GET): If User not found, return NotFound 
        [Fact]
        public async Task Delete_should_return_notfound_when_user_is_missing()
        {
            // Arrange 
            int id = 1;
            var user = (User)null;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert 
            Assert.NotNull(result);
        }

        // Delete (GET): If User found, return View with model 
        [Fact]
        public async Task Delete_should_return_view_with_model_when_user_was_found()
        {
            // Arrange 
            int id = 1;
            var user = new User { Id = id };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act 
            var result = await _controller.Delete(id) as ViewResult;

            // Assert 
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete"
            );
            Assert.Equal(user, result.Model);
        }
    }
}
