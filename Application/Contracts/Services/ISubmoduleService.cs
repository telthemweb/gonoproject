using Application.Dto.Systemmodules;
using Application.Dto.Systemsubmodules;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface ISubmoduleService
    {
        Task<BaseResponse> Modify(ModifySystemsubmoduleDto dto);

        Task<BaseResponse> Delete(int id);
    }
}
