namespace KooliProjekt.Data.Repositories
{
    public class FoodChartRepository : BaseRepository<FoodChart>, IFoodChartRepository
    {
        public FoodChartRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Kasuta neid kahte kui on vaja FoodChart külge objekte laadida (Include käsk)
        //public async Task<PagedResult<FoodChart>> List(int page, int pageSize)
        //{
        //    return await _context.food_Chart.GetPagedAsync(page, pageSize);
        //}

        //public async Task<FoodChart> Get(int id)
        //{
        //    return await _context.food_Chart.FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
