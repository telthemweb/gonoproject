using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Role:IdentityRole
    {
        public List<RoleSystempermission> assignedpermissions { get; set; }

        public List<ApplicationUserRole> applicationuserroles { get; set; }
    }
}
