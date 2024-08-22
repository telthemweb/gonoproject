using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasData(
               new ApplicationUserRole
               {
                   Id=1,
                   RoleId = "c128195d-f38c-4355-a38a-3b1f79c8105c",
                   UserId = "30c8691b-3b28-495c-83c1-6e59833f667b"
               }
            );
        }
    }
}
