using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Collections.Generic;

namespace KooliProjekt.Models
{
    public class UserIndexModel
    {
        public UserSearch Search { get; set; }
        public PagedResult<User> Data { get; set; }
    }
}
