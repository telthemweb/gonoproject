using Application.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class ApplicationUserRole : BaseEntity
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public Role role { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser user { get; set; }
    }
}
