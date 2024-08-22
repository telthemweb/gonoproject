using Application.Data.Common;
using Application.Data.Interfaces;
using Application.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Application.Data.Configurations;

namespace Application
{
   public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly string _username;
        private readonly IHttpContextAccessor _context;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor? httpContextAccessor) : base(options)
        {
            _context = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            if (_context?.HttpContext?.User != null && _context.HttpContext.User.Claims.Count() > 0)
            {
                _username = httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault().Value;
            }
            else
            {
                _username = "Unauthenticated User";
            }
        }
        public DbSet<Systemmodule> systemmodules { get; set; }
        public DbSet<Systemsubmodule> systemsubmodules { get; set; }
        public DbSet<Systempermission> systempermissions { get; set; }

        public DbSet<Emailqueue> emailqueues { get; set; }
        public DbSet<Passwordreset> passwordresets { get; set; }

        public DbSet<RoleSystempermission> rolesystempermissions { get; set; }

        public DbSet<ApplicationUserRole> applicationuserroles { get; set; }



        public DbSet<AuditEntry> auditenties { get; set; }
        public DbSet<Title> titles { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<MaritalStatus> maritalstatuses { get; set; }
        public DbSet<Nationality> nationalities { get; set; }
        public DbSet<Province> provinces { get; set; }
        public DbSet<City> cities { get; set; }

        public DbSet<Visitor> visitors { get; set; }
        public DbSet<VisitorNumber> visitornumbers { get; set; }
        public DbSet<VisitorLogger> visitorloggers { get; set; }
        public DbSet<Systemlog> systemlogs { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //builder.ApplyConfiguration(new RoleConfiguration());
            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new UserRoleConfiguration());
            //builder.ApplyConfiguration(new SystemmoduleConfiguration());
            //builder.ApplyConfiguration(new SystemsubmodleConfiguration());
            builder.Entity<AuditEntry>().Property(a => a.Changes).HasConversion(value => JsonConvert.SerializeObject(value), serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));

            base.OnModelCreating(builder);
        }


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {

            ChangeTracker.DetectChanges();
            var entries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IAuditable))
                    continue;

                if (entry.Entity is BaseEntity)
                {
                    var auditableEntity = entry.Entity as BaseEntity;
                    if (entry.State == EntityState.Added)
                    {

                        if (auditableEntity != null)
                        {
                            auditableEntity.DateCreated = DateTimeOffset.UtcNow;
                        }
                    }
                    if (entry.State == EntityState.Modified)
                    {

                        if (auditableEntity != null)
                        {
                            auditableEntity.DateUpdated = DateTimeOffset.UtcNow;
                        }
                    }
                }
                var auditEntry = new AuditEntry
                {
                    ActionType = entry.State == EntityState.Added ? "INSERT" : entry.State == EntityState.Deleted ? "DELETE" : "UPDATE",
                    EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    Username = _username,
                    TimeStamp = DateTime.UtcNow,
                    Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue),

                    // TempProperties are properties that are only generated on save, e.g. ID's
                    // These properties will be set correctly after the audited entity has been saved
                    TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList(),
                };

                entries.Add(auditEntry);
            }

            return entries;
        }



        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.EntityId = prop.CurrentValue.ToString();
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
            }

            auditenties.AddRange(auditEntries);
            return SaveChangesAsync();
        }
    }
}
