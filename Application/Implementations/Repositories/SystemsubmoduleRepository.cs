using Application;
using Application.Contracts.Persistence;
using Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Repositories
{
    public class SystemsubmoduleRepository : GenericRepository<Systemsubmodule>, ISystemsubmoduleRepository
    {
        public SystemsubmoduleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
