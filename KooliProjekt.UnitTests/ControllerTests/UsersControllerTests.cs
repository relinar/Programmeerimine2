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
        public async Task Index_Should_Return_Correct_View_With_Data()
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
        public async Task Details_Should_Return_NotFound_When_Id_Is_Null()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_User_Not_Found()
        {
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync((User)null);

            var result = await _controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_Should_Return_View_With_User_When_Found()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Details(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }

        [Fact]
        public void Create_Get_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Post_Should_Return_View_When_ModelState_Is_Invalid()
        {
            var user = new User { Id = 1, Name = "New User" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(user) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(user, result?.Model);
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
        public async Task Edit_Get_Should_Return_NotFound_When_Id_Is_Null()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_Should_Return_NotFound_When_User_Not_Found()
        {
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync((User)null);

            var result = await _controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_Should_Return_NotFound_When_Id_Does_Not_Match_User()
        {
            var user = new User { Id = 2, Name = "Updated User" };

            var result = await _controller.Edit(1, user);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_Should_Return_View_When_ModelState_Is_Invalid()
        {
            var user = new User { Id = 1, Name = "Updated User" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(1, user) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(user, result?.Model);
        }

        [Fact]
        public async Task Edit_Post_Should_Update_User_And_Redirect_To_Index()
        {
            var user = new User { Id = 1, Name = "Updated User" };
            _userServiceMock.Setup(x => x.Get(user.Id)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.Save(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _controller.Edit(user.Id, user) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
        [Fact]
        public async Task Delete_Should_Return_View_With_User_When_Found()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Delete(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_Id_Is_Null()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_User_Not_Found()
        {
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync((User)null);

            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Call_Service_And_Redirect()
        {
            _userServiceMock.Setup(x => x.Delete(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
