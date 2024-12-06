// File: Models/FoodChartIndexModel.cs
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class FoodChartIndexModel
    {
        public FoodChartSearch Search { get; set; }  // The search criteria
        public PagedResult<FoodChart> Data { get; set; }  // The paged data
    }
}
