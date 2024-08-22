using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class VisitorLoggerRepository : GenericRepository<VisitorLogger>, IVisitorLoggerRepository
    {
        public VisitorLoggerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
