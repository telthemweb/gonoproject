using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
    {
        public VisitorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
