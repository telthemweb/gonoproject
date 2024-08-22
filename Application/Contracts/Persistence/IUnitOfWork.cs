using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork:IDisposable
    {
      IApplicationUserRepository applicationuserrepository { get;}

      ISystemmoduleRepository systemmodulerepository { get;}

      ISystemsubmoduleRepository systemsystemsubmodulerepository { get;}

      ISystempermissionRepository systempermissionrepository { get;}

      IRoleRepository rolerepository { get;}

      IPasswordresetRepository passwordresetrepository { get;}

      IEmailqueueRepository emailqueuerepository { get;}

      IApplicationUserRoleRepository applicationuserrolerepository { get;}
      IRoleSystempermissionRepository rolesystempermissionrepository { get;}
        ICityRepository cityrepository { get; }
        IGenderRepository genderrepository { get; }
        IMaritalStatusRepository maritalstatusrepository { get; }
        INationalityRepository nationalityRepository { get; }
        IProvinceRepository provincerepository { get; }
        ITitleRepository titlerepository { get; }
        IVisitorRepository visitorRepository { get; }
        IVisitorNumberRepository visitornumberRepository { get; }
        IVisitorLoggerRepository visitorloggerRepository { get; }
        ISystemlogRepository systemlogrepository { get; }
        Task<int> Save();
    }
}
