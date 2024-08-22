using Application.Data;
using Application.Dto.Users;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IApplicationUserService
    {
        Task<List<ApplicationUser>> GetAll();

        Task<ApplicationUser> GetById(string id);

        Task<BaseResponse>ModifyUser(ModifyUserDto dto);

        Task<BaseResponse> ResetPassword(string id);

        Task<BaseResponse> ChangeStatus(string id, string status);

        Task<BaseResponse> AssignRole(string userId, string roleId);    
        Task<BaseResponse> RemoveRole(string userId,string roleId);

    }
}
