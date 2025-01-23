namespace KooliProjekt.Models
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; } // Current page number
        public int PageSize { get; set; }    // Number of items per page
        public int RowCount { get; set; }    // Total number of rows
        public int PageCount { get; set; }   // Total number of pages
        public List<T> Results { get; set; } = new List<T>(); // The actual data for the current page
    }
}
