using Application.Data.Common;
using Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Systempermission : BaseEntity, IAuditable
    {
        public string Name { get; set; }


        public int SystemsubmoduleId { get; set; }

        public Systemsubmodule systemsubmodule { get; set; }

        public List<RoleSystempermission> rolesystempermissions { get; set; }
    }
}
