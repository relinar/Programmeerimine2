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
        public async Task Index_Should_Handle_Exception_Gracefully()
        {
            _userServiceMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearch>()))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.Index(1, new UserIndexModel()) as ViewResult;
            var model = result?.Model as UserIndexModel;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Null(model.Data);
            Assert.True(result.ViewData.ModelState.ContainsKey(""), "Expected a ModelState error.");
        }

        [Fact]
        public async Task Create_Get_Should_Return_View()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_Post_Should_Handle_Exception_Gracefully()
        {
            var user = new User { Id = 1, Name = "New User" };
            _userServiceMock.Setup(x => x.Save(user)).ThrowsAsync(new Exception("Database error"));

            var result = await _controller.Create(user) as ViewResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(result.ViewData.ModelState.ContainsKey(""), "Expected a ModelState error.");
        }

        [Fact]
        public async Task Create_Post_Should_Redirect_When_Successful()
        {
            var user = new User { Id = 1, Name = "New User" };
            _userServiceMock.Setup(x => x.Save(user)).Returns(Task.CompletedTask);

            var result = await _controller.Create(user) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
        [Fact]
        public async Task Create_Post_Should_Return_View_When_ModelState_Invalid()
        {
            var user = new User { Id = 1, Name = "Invalid User" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(user) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(user, result?.Model);
        }

       
        [Fact]
        public async Task Edit_Get_Should_Return_View_When_User_Found()
        {
            var user = new User { Id = 1, Name = "Existing User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Edit(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }
       
        [Fact]
        public async Task Edit_Post_Should_Handle_Exception_And_Return_View()
        {
            var user = new User { Id = 1, Name = "Updated User" };
            _userServiceMock.Setup(x => x.Save(It.IsAny<User>())).ThrowsAsync(new Exception("Database Error"));

            var result = await _controller.Edit(1, user) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(user, result?.Model);
            Assert.True(result.ViewData.ModelState.ContainsKey(""), "Expected a ModelState error.");
        }

        [Fact]
        public async Task Delete_Should_Return_View_When_User_Found()
        {
            var user = new User { Id = 1, Name = "User to Delete" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Delete(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(user.Id, model?.Id);
        }
        
        [Fact]
        public async Task DeleteConfirmed_Should_Handle_Exception_Gracefully()
        {
            _userServiceMock.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception("Database Error"));

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
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
        public async Task Details_Should_Return_NotFound_When_Exception_Occurs()
        {
            _userServiceMock.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            var result = await _controller.Details(1);
            Assert.IsType<ObjectResult>(result);
        }

   
      
         
        [Fact]
        public async Task Details_Should_Return_View_When_User_Exists()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Details(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Edit_Post_Should_Return_View_When_ModelState_Invalid()
        {
            var user = new User { Id = 1, Name = "Invalid User" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(1, user) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(user, result?.Model);
        }
       
        [Fact]
        public async Task Edit_Get_Should_Return_NotFound_When_User_Not_Found()
        {
            _userServiceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((User)null);
            var result = await _controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_Should_Return_View_With_User()
        {
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(x => x.Get(1)).ReturnsAsync(user);

            var result = await _controller.Edit(1) as ViewResult;
            var model = result?.Model as User;

            Assert.NotNull(model);
            Assert.Equal(user.Id, model.Id);
        }

        [Fact]
        public async Task Edit_Post_Should_Update_User_When_Valid()
        {
            var user = new User { Id = 1, Name = "Updated User" };
            _userServiceMock.Setup(x => x.Save(user)).Returns(Task.CompletedTask);

            var result = await _controller.Edit(1, user) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Handle_Exception()
        {
            _userServiceMock.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Delete_User_When_Valid()
        {
            _userServiceMock.Setup(x => x.Delete(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}