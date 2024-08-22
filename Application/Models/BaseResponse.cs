using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BaseResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }

        public List<string>? Errors { get; set; }

        public Object? Result { get; set; }
    }
}
