using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class VisitorNumberRepository : GenericRepository<VisitorNumber>, IVisitorNumberRepository
    {
        public VisitorNumberRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
