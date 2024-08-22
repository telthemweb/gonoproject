using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.RoleSystempermissions;
using Application.Models;
using AutoMapper;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class RoleSystemPermissionService : IRoleSystemPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleSystemPermissionService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {

            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.rolesystempermissionrepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.rolesystempermissionrepository.RemoveAsync(record);

                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Record successfully deleted";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "An error has occured ";
                    }

                }
            }
            catch (Exception ex)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse> Modify(ModifyRoleSystempermissionDto dto)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                int result = 0;
              
                    RoleSystempermission record = await _unitOfWork.rolesystempermissionrepository.GetAsync(q => q.RoleId == dto.RoleId && q.SystempermissionId==dto.SystempermissionId);
                if (record == null)
                {
                    var permission = _mapper.Map<RoleSystempermission>(dto);
                    result = await _unitOfWork.rolesystempermissionrepository.AddAsync(permission);
                    response.Result = permission;

                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Record modification was successful";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "An error has occured failed to save";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Permission already assigned to role";
                }

            }
            catch (Exception ex)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }

            //}
            return response;
        }
    }
}
