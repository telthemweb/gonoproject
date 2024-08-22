using Application.Data;
using Application.Dto.Roles;
using Application.Dto.Systemmodules;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAll();
        Task<BaseResponse> Modify(ModifyRoleDto dto);

        Task<BaseResponse> Delete(string id);
        Task<Role> GetById(string id);

        Task<List<Role>> GetRoleByUserId(string userId);
    }
}
