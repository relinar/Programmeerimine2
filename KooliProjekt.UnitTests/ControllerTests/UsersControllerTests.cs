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
            var userSearch = new UserSearch { Name = "Test User", Role = "Admin" };
            var userIndexModel = new UserIndexModel { Search = userSearch };
            var userList = new List<User>
            {
                new User { Id = 1, Name = "Test User", Role = "Admin" },
                new User { Id = 2, Name = "Test User 2", Role = "User" }
            };
            var pagedResult = new PagedResult<User>(1, 5, 2, userList);
            _userServiceMock.Setup(x => x.List(1, 5, userSearch)).ReturnsAsync(pagedResult);
            
            var result = await _controller.Index(1, userIndexModel) as ViewResult;
            var model = result?.Model as UserIndexModel;
            
            Assert.NotNull(model);
            Assert.Equal(userList, model?.Data?.Results);
            Assert.Equal(userSearch, model?.Search);
        }

        [Fact]
        public async Task Index_should_return_default_view_when_model_is_null()
        {
            var userList = new List<User> { new User { Id = 1, Name = "Test User" } };
            var pagedResult = new PagedResult<User>(1, 5, 1, userList);
            _userServiceMock.Setup(x => x.List(1, 5, null)).ReturnsAsync(pagedResult);

            var result = await _controller.Index(1, null) as ViewResult;
            var model = result?.Model as UserIndexModel;

            Assert.NotNull(model);
            Assert.Equal(userList, model?.Data?.Results);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_null()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_user_when_found()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);
            
            var result = await _controller.Details(1) as ViewResult;
            var model = result?.Model as User;
            
            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }

        [Fact]
        public async Task Create_should_return_view()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Post_Should_Add_User_And_Redirect()
        {
            var user = new User { Id = 1, Name = "New User" };
            _userServiceMock.Setup(x => x.Save(user)).Returns(Task.CompletedTask);
            
            var result = await _controller.Create(user) as RedirectToActionResult;
            
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_null()
        {
            var result = await _controller.Edit(null) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_user_does_not_exist()
        {
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync((User)null);
            var result = await _controller.Edit(1) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_user_when_found()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);
            
            var result = await _controller.Edit(1) as ViewResult;
            var model = result?.Model as User;
            
            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_null()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_user_when_found()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);
            
            var result = await _controller.Delete(1) as ViewResult;
            var model = result?.Model as User;
            
            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_should_call_service_and_redirect()
        {
            _userServiceMock.Setup(x => x.Delete(1)).Returns(Task.CompletedTask);
            
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;
            
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}