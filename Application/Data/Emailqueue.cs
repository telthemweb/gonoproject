using Application.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Emailqueue : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public string HtmlBody { get; set; }

        public List<string> Recepiants { get; set; }
        public string Status { get; set; }
    }
}
