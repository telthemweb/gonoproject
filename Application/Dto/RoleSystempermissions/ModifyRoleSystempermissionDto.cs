using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.RoleSystempermissions
{
    public class ModifyRoleSystempermissionDto : BaseDto
    {
        public string RoleId { get; set; }

        public int SystempermissionId { get; set; }
    }
}
