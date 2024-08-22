using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class MaritalStatusRepository : GenericRepository<MaritalStatus>, IMaritalStatusRepository
    {
        public MaritalStatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}