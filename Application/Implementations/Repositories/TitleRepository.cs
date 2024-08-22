using Application.Contracts.Persistence;
using Application.Data;

namespace Application.Implementations.Repositories
{
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
