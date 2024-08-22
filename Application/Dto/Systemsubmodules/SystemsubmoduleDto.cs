using Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemsubmodules
{
    public class SystemsubmoduleDto : BaseDto
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }
        public string AccessName { get; set; }

    }
}
