using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class AmountServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_amount()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount
            {
                NutrientsID = 1,
                AmountDate = DateTime.Now
            };

            // Act
            await service.Save(amount);

            // Assert
            var count = DbContext.amount.Count();
            var result = DbContext.amount.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(amount.NutrientsID, result.NutrientsID);
            Assert.Equal(amount.AmountDate, result.AmountDate);
        }

        [Fact]
        public async Task Save_should_update_existing_amount()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount
            {
                AmountID = 1,
                NutrientsID = 1,
                AmountDate = DateTime.Now
            };

            DbContext.amount.Add(amount);
            DbContext.SaveChanges();

            // Update the amount
            amount.NutrientsID = 2;

            // Act
            await service.Save(amount);

            // Assert
            var updatedAmount = DbContext.amount.FirstOrDefault(a => a.AmountID == 1);
            Assert.NotNull(updatedAmount);
            Assert.Equal(2, updatedAmount.NutrientsID);
        }

        [Fact]
        public async Task Delete_should_remove_given_amount()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount
            {
                NutrientsID = 1,
                AmountDate = DateTime.Now
            };

            DbContext.amount.Add(amount);
            DbContext.SaveChanges();

            // Act
            await service.Delete(amount.AmountID);

            // Assert
            var count = DbContext.amount.Count();
            Assert.Equal(0, count); // Ensure it is deleted
        }
        [Fact]
        public async Task Delete_should_not_throw_exception_for_non_existing_amount()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Act: Try to delete an amount with a non-existing ID (e.g. 999)
            await service.Delete(999);

            // Assert: Ensure no exception was thrown, and the operation completed successfully
            // Since the service doesn't throw an exception, we are just ensuring the method doesn't break
            var amount = await DbContext.amount.FindAsync(999);
            Assert.Null(amount);  // Ensure the amount still doesn't exist
        }
      

        [Fact]
        public async Task Get_should_return_null_for_non_existing_amount()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Act
            var result = await service.Get(999); // Non-existing ID

            // Assert
            Assert.Null(result);
        }
    }
}
