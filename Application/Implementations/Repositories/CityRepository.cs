using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}