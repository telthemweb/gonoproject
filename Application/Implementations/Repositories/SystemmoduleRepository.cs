using Application;
using Application.Contracts.Persistence;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Repositories
{
    public class SystemmoduleRepository : GenericRepository<Systemmodule>, ISystemmoduleRepository
    {
        private readonly IMemoryCache _memoryCache;
        public SystemmoduleRepository(AppDbContext dbContext, IMemoryCache memoryCache) : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<Systemmodule>> GetCachedAll()
        {
            
                string key = "systemmodules";

                return await _memoryCache.GetOrCreateAsync(key, async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                    return await _dbContext.systemmodules.Include(q => q.submodules).ToListAsync();
                });
            
        }

        public async Task<List<Systemmodule>> GetModulesByRole(string roleId)
        {
           var response = await _dbContext.systemmodules
                                         .Include(q => q.submodules)
                                         .ThenInclude(q=>q.systempermissions)
                                         .ThenInclude(q=>q.rolesystempermissions.Where(q=>q.RoleId==roleId))
                                         .ToListAsync();
            return response;
        }
    }
}
