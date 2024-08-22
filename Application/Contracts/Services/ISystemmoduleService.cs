using Application.Data;
using Application.Dto.Systemmodules;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface ISystemmoduleService
    {
        Task<List<Systemmodule>> GetAll();
        Task<List<Systemmodule>> GetCached();

        Task<List<Systemmodule>> GetByRoleId(string roleId);
        Task<BaseResponse> Modify(ModifySystemmoduleDto dto);

        Task<BaseResponse> Delete(int id);
    }
}
