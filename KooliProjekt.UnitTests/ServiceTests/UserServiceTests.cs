using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Threading.Tasks;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Search;
using System.Collections.Generic;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_unitOfWorkMock.Object);

            // Set up the UnitOfWork mock to return the UserRepository mock
            _unitOfWorkMock.Setup(uow => uow.Users).Returns(_userRepositoryMock.Object);
        }
        [Fact]
        public async Task List_ShouldReturnPagedUsers()
        {
            // Arrange
            var search = new UserSearch { Name = "John" };
            var users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Role = "Admin" },
        new User { Id = 2, Name = "John Smith", Role = "User" }
    };
            var pagedResult = new PagedResult<User>
            {
                Results = users,
                CurrentPage = 1,
                PageSize = 10,
                RowCount = 2,
                PageCount = 1
            };
            _userRepositoryMock.Setup(repo => repo.List(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearch>()))
                               .ReturnsAsync(pagedResult);

            // Act
            var result = await _userService.List(1, 10, search);

            // Assert
            Assert.Equal(2, result.RowCount);
            Assert.Equal(1, result.PageCount);
            Assert.Equal("John Doe", result.Results[0].Name);
        }
        [Fact]
        public async Task Get_ShouldReturnUserForValidId()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Role = "Admin" };
            _userRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("Admin", result.Role);
        }
        [Fact]
        public async Task Save_ShouldAddNewUserWhenIdIsZero()
        {
            // Arrange
            var user = new User { Id = 0, Name = "Jane Doe", Role = "User" };
            _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            await _userService.Save(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingUserWhenIdIsNonZero()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Jane Doe", Role = "User" };
            _userRepositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            await _userService.Save(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once);
        }
        [Fact]
        public async Task Delete_ShouldDeleteUserWhenUserExists()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new User { Id = 1, Name = "John Doe", Role = "Admin" });
            _userRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _userService.Delete(1);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldDoNothingWhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((User)null); // Simulate user not found

            // Act
            await _userService.Delete(999);  // Pass a non-existing ID

            // Assert
            _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Never);  // Ensure Delete() was not called
        }

    }
}
