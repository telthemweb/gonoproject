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
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "30c8691b-3b28-495c-83c1-6e59833f667b",
                    Email = "administrator@pe.ac.zw",
                    Name = "Admin",
                    Title = "Mr",
                    Surname = "Admin",
                    UserName = "administrator@pe.ac.zw",
                    NormalizedEmail = "ADMINISTRATOR@PE.AC.ZW",
                    NormalizedUserName = "ADMINISTRATOR@PE.AC.ZW",
                    Status = "ACTIVE",
                    PasswordExpireDate = DateTime.UtcNow.AddMonths(12),
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true,

                }
            );
        }

    }
}
