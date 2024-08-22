using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.Configurations
{
    public class SystemmoduleConfiguration : IEntityTypeConfiguration<Systemmodule>
    {
        public void Configure(EntityTypeBuilder<Systemmodule> builder)
        {
            builder.HasData(
               new Systemmodule
               {
                   Id = 1,
                   Name = "Global Settings",
                   Icon = "fa fa-gears",
                   AccessName = "Generalsetting.Index"
               },
                new Systemmodule
                {
                    Id = 1,
                    Name = "Visitors",
                    Icon = "fa fa-users",
                    AccessName = "Visitor.Index"
                },
                 new Systemmodule
                 {
                     Id = 1,
                     Name = "Reports",
                     Icon = "fa fa-file",
                     AccessName = "Report.Index"
                 }
             );
        }
    }
}
