using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            page = Math.Max(page, 1);
            pageSize = pageSize == 0 ? 10 : pageSize;

            var result = new PagedResult<T>(
                currentPage: page,
                pageSize: pageSize,
                rowCount: await query.CountAsync(),
                results: await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
            );

            return result;
        }
    }
}
