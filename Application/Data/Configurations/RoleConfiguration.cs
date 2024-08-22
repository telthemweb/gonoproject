using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "c4c33c27-8292-4278-af7a-279b6e9bb689",
                    Name = "User",
                    NormalizedName = "USER"
                },
                  new Role
                  {
                      Id = "c128195d-f38c-4355-a38a-3b1f79c8105c",
                      Name = "Administrator",
                      NormalizedName = "ADMINISTRATOR"
                  }

               );
        }
    }
}
