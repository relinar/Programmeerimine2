using KooliProjekt.Data;

public class PagedResult<T> : PagedResultBase where T : class
{
    public List<T> Results { get; set; }
}
