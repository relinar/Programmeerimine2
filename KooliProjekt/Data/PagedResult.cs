public class PagedResult<T> where T : class
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public int PageCount => RowCount == 0 ? 1 : (int)Math.Ceiling((double)RowCount / PageSize); // Ensure PageCount is at least 1
    public List<T> Results { get; set; }

    public PagedResult(int currentPage, int pageSize, int rowCount, List<T> results)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        RowCount = rowCount;
        Results = results ?? new List<T>();  // Ensure Results is never null
    }
}
