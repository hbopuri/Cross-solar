using CrossSolar.Domain;
using CrossSolar.Models;

namespace CrossSolar.Repository
{
    public class DayAnalyticsRepository : GenericRepository<OneDayElectricityModel>, IDayAnalyticsRepository
    {
        public DayAnalyticsRepository(CrossSolarDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}