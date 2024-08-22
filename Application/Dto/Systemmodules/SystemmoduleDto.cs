using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemmodules
{
    public class SystemmoduleDto:BaseDto
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public string AccessName { get; set; }
    }
}
