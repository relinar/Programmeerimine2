using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class AmountServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IAmountRepository> _repositoryMock;
        private readonly AmountService _amountService;

        public AmountServiceTests()
        {
            _repositoryMock = new Mock<IAmountRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // Setup the unit of work mock to return the mocked AmountRepository
            _unitOfWorkMock.Setup(uow => uow.Amounts).Returns(_repositoryMock.Object);

            _amountService = new AmountService(_unitOfWorkMock.Object);
        }
       
       
        [Fact]
        public async Task GetAmountsAsync_ShouldReturnCorrectAmounts()
        {
            // Arrange
            var amounts = new List<Amount>
            {
                new Amount { AmountID = 1, NutrientsID = 101, AmountDate = DateTime.Now },
                new Amount { AmountID = 2, NutrientsID = 102, AmountDate = DateTime.Now }
            };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(amounts);

            // Act
            var result = await _amountService.GetAmountsAsync();

            // Assert
            Assert.Equal(amounts.Count, result.Count);
            Assert.Equal(amounts.First().AmountID, result.First().AmountID);
        }

        // 3. Get
        [Fact]
        public async Task Get_ShouldReturnAmount_WhenAmountExists()
        {
            // Arrange
            var amount = new Amount { AmountID = 1, NutrientsID = 101, AmountDate = DateTime.Now };
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(amount);

            // Act
            var result = await _amountService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(amount.AmountID, result.AmountID);
        }

        // 4. AddAmountAsync
        [Fact]
        public async Task AddAmountAsync_ShouldAddAmount()
        {
            // Arrange
            var amount = new Amount { AmountID = 3, NutrientsID = 103, AmountDate = DateTime.Now };
            _repositoryMock.Setup(r => r.SaveAsync(It.IsAny<Amount>())).Returns(Task.CompletedTask);

            // Act
            await _amountService.AddAmountAsync(amount);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(amount), Times.Once);
        }

        // 5. UpdateAmountAsync
        [Fact]
        public async Task UpdateAmountAsync_ShouldUpdateAmount()
        {
            // Arrange
            var amount = new Amount { AmountID = 1, NutrientsID = 101, AmountDate = DateTime.Now };
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(amount);
            _repositoryMock.Setup(r => r.SaveAsync(It.IsAny<Amount>())).Returns(Task.CompletedTask);

            // Act
            await _amountService.UpdateAmountAsync(amount);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(amount), Times.Once);
        }

        // 6. Delete
        [Fact]
        public async Task Delete_ShouldDeleteAmount()
        {
            // Arrange
            var amount = new Amount { AmountID = 1, NutrientsID = 101, AmountDate = DateTime.Now };
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(amount);
            _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _amountService.Delete(1);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        // 7. Save (either Add or Update)
        [Fact]
        public async Task Save_ShouldSaveAmount_WhenNew()
        {
            // Arrange
            var amount = new Amount { AmountID = 3, NutrientsID = 104, AmountDate = DateTime.Now };
            _repositoryMock.Setup(r => r.SaveAsync(It.IsAny<Amount>())).Returns(Task.CompletedTask);

            // Act
            await _amountService.Save(amount);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(amount), Times.Once);
        }
    }
}
