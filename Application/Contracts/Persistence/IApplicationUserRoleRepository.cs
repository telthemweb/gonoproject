using Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IApplicationUserRoleRepository:IGenericRepository<ApplicationUserRole>
    {
        Task<IEnumerable<ApplicationUserRole>> GetWithPermissions(string UserId);
    }
}
