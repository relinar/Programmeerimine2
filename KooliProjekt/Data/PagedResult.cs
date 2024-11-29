namespace KooliProjekt.Data
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Results { get; set; }  // See sisaldab andmeid (nt kasutajaid)
        public int TotalCount { get; set; }  // Kogu arv
        public int PageIndex { get; set; }  // Praegune leht
        public int TotalPages { get; set; } // Koguarv lehti
    }
}
