using Application.Data.Common;
using Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class RoleSystempermission : BaseEntity, IAuditable
    {
        public string RoleId { get; set; }

        public int SystempermissionId { get; set; }

        public Systempermission permission { get; set; }
    }
}
