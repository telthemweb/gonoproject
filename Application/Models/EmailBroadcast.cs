using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmailBroadcast
    {
        public List<string> emails { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        public string? Attachment { get; set; }
    }
}
