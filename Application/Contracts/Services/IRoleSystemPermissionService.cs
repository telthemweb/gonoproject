using Application.Dto.Roles;
using Application.Dto.RoleSystempermissions;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IRoleSystemPermissionService
    {
        Task<BaseResponse> Modify(ModifyRoleSystempermissionDto dto);

        Task<BaseResponse> Delete(int id);
    }
}
