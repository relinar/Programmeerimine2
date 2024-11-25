using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Data
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            // Ensure page is at least 1
            page = Math.Max(page, 1);

            // Default page size to 10 if not specified
            pageSize = pageSize == 0 ? 10 : pageSize;

            // Prepare a new PagedResult object to hold the data
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync() // Get total row count from the query
            };

            // Calculate the total page count based on row count and page size
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            // Get the items for the current page
            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(); // Get the paged data

            return result;
        }
    }
}
