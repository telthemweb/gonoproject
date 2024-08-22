using Application.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Passwordreset : BaseEntity
    {
        public string Email { get; set; }

        public string Uuid { get; set; }

        public string Token { get; set; }

        public DateOnly ExpireDate { get; set; }
    }
}
