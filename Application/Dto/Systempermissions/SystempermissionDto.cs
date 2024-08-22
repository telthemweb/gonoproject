using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systempermissions
{
    public class SystempermissionDto:BaseDto
    {
        public string Name { get; set; }

        public int SystemsubmoduleId { get; set; }
    }
}
