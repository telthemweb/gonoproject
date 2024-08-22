using Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface ISystemmoduleRepository:IGenericRepository<Systemmodule>
    {
        Task<List<Systemmodule>> GetCachedAll();
        Task<List<Systemmodule>> GetModulesByRole(string roleId);
    }
}
