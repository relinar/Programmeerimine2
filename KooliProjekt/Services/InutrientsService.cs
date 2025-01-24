using System.Threading.Tasks;
using KooliProjekt.Models;
using System.Collections.Generic;
using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface INutrientsService
    {
        // Existing method for listing nutrients
        Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search);

        // Add a new nutrient
        Task Add(Nutrients nutrient);

        // Update an existing nutrient
        Task Update(Nutrients nutrient);

        // Delete a nutrient by ID
        Task Delete(int id);

        // Get a specific nutrient by ID
        Task<Nutrients> Get(int id);
    }
}
