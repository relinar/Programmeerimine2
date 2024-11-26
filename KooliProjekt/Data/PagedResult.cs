namespace KooliProjekt.Data
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Results { get; set; }
    }
}