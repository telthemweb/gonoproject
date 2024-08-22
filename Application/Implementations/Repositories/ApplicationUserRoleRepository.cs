using Application.Contracts.Persistence;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Repositories
{
    public class ApplicationUserRoleRepository : GenericRepository<ApplicationUserRole>, IApplicationUserRoleRepository
    {
        public ApplicationUserRoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ApplicationUserRole>> GetWithPermissions(string UserId)
        {
            var response = await _dbContext.applicationuserroles
                                                      .Include(q=>q.role)
                                                      .ThenInclude(q=>q.assignedpermissions)
                                                      .ThenInclude(q=>q.permission)
                                                      .Where(q=>q.UserId==UserId).ToListAsync();
            return response;
        }
    }
}
