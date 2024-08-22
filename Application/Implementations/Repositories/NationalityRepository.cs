using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class NationalityRepository : GenericRepository<Nationality>, INationalityRepository
    {
        public NationalityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}