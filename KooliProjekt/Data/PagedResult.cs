// File: Models/PagedResult.cs
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Results { get; set; }
    }
}
