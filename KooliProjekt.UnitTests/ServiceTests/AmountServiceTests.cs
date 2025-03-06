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

            [Fact]
            public async Task List_should_return_paginated_results()
            {
                // Arrange
                var service = new AmountService(DbContext);
                // Seed some amounts into the database for pagination
                DbContext.amount.AddRange(new List<Amount>
            {
                new Amount { NutrientsID = 1, AmountDate = DateTime.Now },
                new Amount { NutrientsID = 2, AmountDate = DateTime.Now },
                new Amount { NutrientsID = 3, AmountDate = DateTime.Now },
                new Amount { NutrientsID = 4, AmountDate = DateTime.Now },
                new Amount { NutrientsID = 5, AmountDate = DateTime.Now }
            });
                await DbContext.SaveChangesAsync();

                // Act
                var page1 = await service.List(1, 2, new amountSearch());
                var page2 = await service.List(2, 2, new amountSearch());

                // Assert
                Assert.Equal(2, page1.Results.Count);  // Page 1 should have 2 results
                Assert.Equal(2, page2.Results.Count);  // Page 2 should also have 2 results
                Assert.Equal(5, page1.RowCount);  // Total count should be 5
                Assert.Equal(5, page2.RowCount);  // Total count should be 5
                Assert.Equal(3, page1.PageCount);  // PageCount should be 3 (5 items, 2 per page)
                Assert.Equal(3, page2.PageCount);  // PageCount should be 3
            }
        [Fact]
        public async Task List_should_return_filtered_results_based_on_search()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed some amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            // Set search filter for NutrientsID = 2
            var search = new amountSearch
            {
                NutrientsID = 2
            };

            // Act
            var result = await service.List(1, 10, search);  // Searching for NutrientsID = 2

            // Assert
            Assert.Single(result.Results);  // Only 1 result should be found
            Assert.Equal(2, result.Results.First().NutrientsID);  // Ensure the correct NutrientsID is returned

            // Adjust PageCount assertion:
            // Since we're only filtering on NutrientsID = 2, the RowCount should be 1 (matching result),
            // and the PageCount will be calculated as 1 (1 result per page, 1 total result).
            Assert.Equal(1, result.PageCount);  // PageCount should be 1, since only one result matches the filter
        }
       
        [Fact]
        public async Task List_should_support_pagination()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed more amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            var amount4 = new Amount { NutrientsID = 4, AmountDate = DateTime.Now.AddDays(3) };
            var amount5 = new Amount { NutrientsID = 5, AmountDate = DateTime.Now.AddDays(4) };
            DbContext.amount.AddRange(amount1, amount2, amount3, amount4, amount5);
            await DbContext.SaveChangesAsync();

            // Act - Get page 1, with page size of 2
            var result = await service.List(1, 2, new amountSearch());

            // Assert
            Assert.Equal(2, result.Results.Count); // We should get 2 results for page 1
            Assert.Equal(3, result.PageCount);  // Total 5 items, with 2 per page, there should be 3 pages
            Assert.Equal(5, result.RowCount);  // Total 5 results
        }

        [Fact]
        public async Task List_should_return_filtered_results_when_AmountID_is_specified()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed amounts into the database
            var amount1 = new Amount { AmountID = 1, NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { AmountID = 2, NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { AmountID = 3, NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            // Search filter for AmountID
            var search = new amountSearch { AmountID = 2 };

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Single(result.Results);  // Only 1 result should match
            Assert.Equal(2, result.Results.First().AmountID);  // Ensure correct AmountID is returned
        }
        [Fact]
        public async Task List_should_return_all_results_in_one_page_if_pageSize_is_greater_than_results()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            DbContext.amount.AddRange(amount1, amount2);
            await DbContext.SaveChangesAsync();

            var search = new amountSearch();  // No filters applied

            // Act: Request a page size larger than the results
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Equal(2, result.Results.Count);  // All results should be returned
            Assert.Equal(1, result.PageCount);  // Only one page, since all results fit on it
        }

        [Fact]
        public async Task List_should_return_all_results_when_no_filters_are_applied()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            DbContext.amount.AddRange(amount1, amount2);
            await DbContext.SaveChangesAsync();

            var search = new amountSearch();  // No filters applied

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Equal(2, result.Results.Count);  // Both amounts should be returned
            Assert.Equal(1, result.PageCount);  // All results fit in one page
        }

        [Fact]
        public async Task List_should_return_filtered_results_based_on_AmountDate()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            var search = new amountSearch { AmountDate = DateTime.Now.AddDays(1) }; // Only amount2 should match

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Single(result.Results);  // Only 1 result should be found
            Assert.Equal(amount2.NutrientsID, result.Results.First().NutrientsID);  // Ensure the correct NutrientsID is returned
            Assert.Equal(1, result.PageCount);  // Only one page with matching results
        }
        [Fact]
        public async Task List_should_return_empty_results_for_non_existing_NutrientsID()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            DbContext.amount.AddRange(amount1, amount2);
            await DbContext.SaveChangesAsync();

            var search = new amountSearch { NutrientsID = 999 }; // Non-existing NutrientsID

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Empty(result.Results); // No results should be found
            Assert.Equal(0, result.PageCount); // PageCount should be 1, even with no results, as long as there's pagination
        }


        [Fact]
        public async Task List_should_return_empty_results_for_non_existing_AmountID()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            DbContext.amount.Add(amount1);
            await DbContext.SaveChangesAsync();

            var search = new amountSearch { AmountID = 999 }; // Non-existing AmountID

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Empty(result.Results); // No results should be found
            Assert.Equal(0, result.PageCount); // PageCount should be 1, even with no results, as long as there's pagination
        }

        [Fact]
        public async Task List_should_return_correct_results_for_multiple_pages()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            var amount4 = new Amount { NutrientsID = 4, AmountDate = DateTime.Now.AddDays(3) };
            var amount5 = new Amount { NutrientsID = 5, AmountDate = DateTime.Now.AddDays(4) };

            DbContext.amount.AddRange(amount1, amount2, amount3, amount4, amount5);
            await DbContext.SaveChangesAsync();

            // Act: Request the first page with a page size of 2
            var resultPage1 = await service.List(1, 2, new amountSearch());
            var resultPage2 = await service.List(2, 2, new amountSearch());

            // Assert: First page should have 2 results
            Assert.Equal(2, resultPage1.Results.Count);
            Assert.Equal(1, resultPage1.CurrentPage);
            Assert.Equal(3, resultPage1.PageCount); // 5 items / 2 per page = 3 pages

            // Assert: Second page should have the next 2 results
            Assert.Equal(2, resultPage2.Results.Count);
            Assert.Equal(2, resultPage2.CurrentPage);
        }
        [Fact]
        public async Task GetAmountsAsync_ShouldReturnAllAmounts()
        {
            // Arrange
            var service = new AmountService(DbContext);
            DbContext.amount.AddRange(new Amount { NutrientsID = 1 }, new Amount { NutrientsID = 2 });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.GetAmountsAsync();

            // Assert
            Assert.Equal(2, result.Count);  // Should return 2 amounts
        }
        [Fact]
        public async Task Save_ShouldCommitChangesToDatabase()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };

            // Act
            await service.Save(amount);  // Make sure to pass 'Amount' object

            // Assert
            var savedAmount = await DbContext.amount.FindAsync(amount.AmountID);  // Use 'AmountID' instead of 'Id'
            Assert.NotNull(savedAmount);  // The amount should be saved to the database
        }

        [Fact]
        public async Task List_ShouldReturnPagedResults()
        {
            // Arrange
            var service = new AmountService(DbContext);
            DbContext.amount.AddRange(new Amount { NutrientsID = 1 }, new Amount { NutrientsID = 2 });
            await DbContext.SaveChangesAsync();

            var search = new amountSearch();  // No filters for this example

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Equal(2, result.RowCount);  // Should return 2 amounts
            Assert.Equal(1, result.PageCount);  // Only 1 page of results
        }

        [Fact]
        public async Task Delete_ShouldRemoveAmountFromDatabase()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            DbContext.amount.Add(amount);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(amount.AmountID);  // Use 'AmountID' instead of 'Id'

            // Assert
            var deletedAmount = await DbContext.amount.FindAsync(amount.AmountID);  // Use 'AmountID' instead of 'Id'
            Assert.Null(deletedAmount);  // The amount should be null (deleted)
        }



        [Fact]
        public async Task UpdateAmountAsync_ShouldUpdateAmountInDatabase()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            DbContext.amount.Add(amount);
            await DbContext.SaveChangesAsync();

            // Update the amount
            amount.NutrientsID = 2;

            // Act
            await service.UpdateAmountAsync(amount);

            // Assert
            var updatedAmount = await DbContext.amount.FindAsync(amount.AmountID);  // Use 'AmountID' instead of 'Id'
            Assert.NotNull(updatedAmount);
            Assert.Equal(2, updatedAmount.NutrientsID);  // NutrientsID should be updated
        }


        [Fact]
        public async Task AddAmountAsync_ShouldAddAmountToDatabase()
        {
            // Arrange
            var service = new AmountService(DbContext);
            var amount = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };

            // Act
            await service.AddAmountAsync(amount);

            // Assert
            var result = await DbContext.amount.FindAsync(amount.AmountID);  // Use 'AmountID' instead of 'Id'
            Assert.NotNull(result);  // Amount should be found in the database
            Assert.Equal(amount.NutrientsID, result.NutrientsID);  // NutrientsID should match
        }


        [Fact]
        public async Task List_should_return_empty_result_if_no_data_matches_filter()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            DbContext.amount.AddRange(amount1, amount2);
            await DbContext.SaveChangesAsync();

            // Search filter for NutrientsID that does not exist
            var search = new amountSearch { NutrientsID = 999 };  // No record with NutrientsID 999

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Empty(result.Results);  // No results should be returned
            Assert.Equal(0, result.RowCount);  // RowCount should be 0 for no results
            Assert.Equal(0, result.PageCount);  // PageCount should be 1, as the first page is considered (even empty)
        }


        [Fact]
        public async Task List_should_support_pagination_with_filters()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed more amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            var amount4 = new Amount { NutrientsID = 4, AmountDate = DateTime.Now.AddDays(3) };
            var amount5 = new Amount { NutrientsID = 5, AmountDate = DateTime.Now.AddDays(4) };
            DbContext.amount.AddRange(amount1, amount2, amount3, amount4, amount5);
            await DbContext.SaveChangesAsync();

            // Search filter for NutrientsID
            var search = new amountSearch { NutrientsID = 2 };  // Only items with NutrientsID = 2 will match

            // Act - Pagination: page 1 with page size 2
            var result = await service.List(1, 2, search);

            // Assert
            Assert.Equal(1, result.Results.Count);  // Only 1 result matches NutrientsID = 2
            Assert.Equal(1, result.PageCount);  // Only 1 page with the filtered result
        }

        [Fact]
        public async Task List_should_return_filtered_results_when_AmountDate_is_specified()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            // Search filter for AmountDate
            var search = new amountSearch { AmountDate = DateTime.Now.AddDays(1) };

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Single(result.Results);  // Only 1 result should match
            Assert.Equal(DateTime.Now.AddDays(1).Date, result.Results.First().AmountDate.Date);  // Ensure correct AmountDate is returned
        }

        [Fact]
        public async Task List_should_return_filtered_results_when_NutrientsID_is_specified()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            // Search filter for NutrientsID
            var search = new amountSearch { NutrientsID = 2 };

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Single(result.Results);  // Only 1 result should match
            Assert.Equal(2, result.Results.First().NutrientsID);  // Ensure correct NutrientsID is returned
        }

        [Fact]
        public async Task List_should_return_all_amounts_when_no_filter_is_applied()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed some amounts into the database
            var amount1 = new Amount { NutrientsID = 1, AmountDate = DateTime.Now };
            var amount2 = new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) };
            var amount3 = new Amount { NutrientsID = 3, AmountDate = DateTime.Now.AddDays(2) };
            DbContext.amount.AddRange(amount1, amount2, amount3);
            await DbContext.SaveChangesAsync();

            // Act - No search parameters
            var result = await service.List(1, 10, new amountSearch());

            // Assert
            Assert.Equal(3, result.RowCount); // Ensure we have 3 items
            Assert.Equal(1, result.PageCount); // Only one page
            Assert.Equal(3, result.Results.Count); // All 3 results should be in the list
        }

        [Fact]
        public async Task List_should_return_empty_list_for_non_matching_search()
        {
            // Arrange
            var service = new AmountService(DbContext);

            // Seed some amounts into the database
            DbContext.amount.AddRange(new List<Amount>
    {
        new Amount { NutrientsID = 1, AmountDate = DateTime.Now },
        new Amount { NutrientsID = 2, AmountDate = DateTime.Now.AddDays(1) }
    });
            await DbContext.SaveChangesAsync();

            var search = new amountSearch
            {
                NutrientsID = 99  // Non-existing NutrientsID
            };

            // Act
            var result = await service.List(1, 10, search);

            // Assert
            Assert.Empty(result.Results);  // No results should be found
            Assert.Equal(0, result.PageCount);  // PageCount should be 1 for no matching results (empty page)
            Assert.Equal(0, result.RowCount);  // No matching rows, RowCount should be 0
        }

    }
}

