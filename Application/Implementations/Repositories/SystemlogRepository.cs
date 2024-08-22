using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class SystemlogRepository : GenericRepository<Systemlog>, ISystemlogRepository
    {
        public SystemlogRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
