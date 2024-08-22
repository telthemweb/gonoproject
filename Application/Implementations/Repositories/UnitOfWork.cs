using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Implementations.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IDbContextFactory<AppDbContext> _context;
        public readonly AppDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        public UnitOfWork(IDbContextFactory<AppDbContext> context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _dbContext = _context.CreateDbContext();
            applicationuserrepository = new ApplicationUserRepository(_dbContext);
            systemmodulerepository = new SystemmoduleRepository(_dbContext,_memoryCache);
            systemsystemsubmodulerepository = new SystemsubmoduleRepository(_dbContext);
            systempermissionrepository = new SystempermissionRepository(_dbContext);
            rolerepository = new RoleRepository(_dbContext);
            passwordresetrepository = new PasswordresetRepository(_dbContext);
            emailqueuerepository = new EmailqueueRepository(_dbContext);
            applicationuserrepository = new ApplicationUserRepository(_dbContext);
            applicationuserrolerepository = new ApplicationUserRoleRepository(_dbContext);
            rolesystempermissionrepository = new RoleSystempermissionRepository(_dbContext);
            cityrepository = new CityRepository(_dbContext);
            genderrepository = new GenderRepository(_dbContext);
            maritalstatusrepository = new MaritalStatusRepository(_dbContext);
            nationalityRepository = new NationalityRepository(_dbContext);
            provincerepository = new ProvinceRepository(_dbContext);
            titlerepository = new TitleRepository(_dbContext);
            visitorRepository = new VisitorRepository(_dbContext);
            visitornumberRepository = new VisitorNumberRepository(_dbContext);
            visitorloggerRepository = new VisitorLoggerRepository(_dbContext);
            systemlogrepository= new SystemlogRepository(_dbContext);
        }

        public IApplicationUserRepository applicationuserrepository { get; private set; }

        public ISystemmoduleRepository systemmodulerepository { get; private set; }

        public ISystemsubmoduleRepository systemsystemsubmodulerepository { get; private set; }

        public ISystempermissionRepository systempermissionrepository { get; private set; }

        public IRoleRepository rolerepository { get; private set; }

        public IPasswordresetRepository passwordresetrepository { get; private set; }

        public IEmailqueueRepository emailqueuerepository { get; private set; }

        public IApplicationUserRoleRepository applicationuserrolerepository { get; private set; }

        public IRoleSystempermissionRepository rolesystempermissionrepository { get; private set; }

        public ICityRepository cityrepository { get; private set; }
        public IGenderRepository genderrepository { get; private set; }
        public IMaritalStatusRepository maritalstatusrepository { get; private set; }
        public INationalityRepository nationalityRepository { get; private set; }
        public IProvinceRepository provincerepository { get; private set; }
        public ITitleRepository titlerepository { get; private set; }
        public IVisitorRepository visitorRepository { get; private set; }
        public IVisitorNumberRepository visitornumberRepository { get; private set; }
        public IVisitorLoggerRepository visitorloggerRepository { get; private set; }
        public ISystemlogRepository systemlogrepository { get; private set; }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }

}
