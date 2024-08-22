using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Configurations
{
    public class SystemsubmodleConfiguration : IEntityTypeConfiguration<Systemsubmodule>
    {
        public void Configure(EntityTypeBuilder<Systemsubmodule> builder)
        {
            builder.HasData(
            new Systemsubmodule
            {
                Id = 1,
                SystemmoduleId = 1,
                Name = "System modules",
                Icon = "fa fa-link",
                Url = "/systemmodules",
                AccessName = "Systemmodule.Access"
            },
                 new Systemsubmodule
                 {
                     Id = 2,
                     SystemmoduleId = 1,
                     Name = "Users",
                     Icon = "fa fa-users",
                     Url = "/users",
                     AccessName = "Users.Access"
                 }
                 ,
                 new Systemsubmodule
                 {
                     Id = 3,
                     SystemmoduleId = 1,
                     Name = "Roles",
                     Icon = "fa fa-lock",
                     Url = "/roles",
                     AccessName = "Roles.Access"
                 }
                 );

        }
    }
}
