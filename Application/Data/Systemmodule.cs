using Application.Data.Common;
using Application.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Systemmodule : BaseEntity, IAuditable
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public string AccessName { get; set; }

        public List<Systemsubmodule> submodules { get; set; }
    }
}
