using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI
{
    public interface IApiClient
    {
        Task<Result<List<Amount>>> List();
        Task<Result> Save(Amount amount);
        Task<Result> Delete(int id);
    }
}
