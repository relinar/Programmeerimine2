namespace KooliProjekt.Data
{
    public class PagedResult<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int PageCount => (int)Math.Ceiling((double)RowCount / PageSize); // Dynamically calculate page count
        public List<T> Results { get; set; }

        // Constructor that does not include pageCount (calculated dynamically)
        public PagedResult(int currentPage, int pageSize, int rowCount, List<T> results)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            RowCount = rowCount;
            Results = results ?? new List<T>(); // Ensure that Results is never null
        }
    }
}
