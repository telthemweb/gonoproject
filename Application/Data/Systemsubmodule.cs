using Application.Data.Common;
using Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Systemsubmodule : BaseEntity, IAuditable
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public int SystemmoduleId { get; set; }
        public string Url { get; set; }
        public string AccessName { get; set; }

        public Systemmodule systemmodule { get; set; }
        public List<Systempermission> systempermissions { get; set; }
    }
}
