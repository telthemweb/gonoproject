using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.Systempermissions;
using Application.Models;
using AutoMapper;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class SystempermissionService : ISystempermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SystempermissionService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.systempermissionrepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.systempermissionrepository.RemoveAsync(record);

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

        public async Task<BaseResponse> Modify(ModifySystempermissionDto dto)
        {
            var response = new BaseResponse();
            try
            {

                int result = 0;
                await SaveModulePermission(dto.SystemsubmoduleId);
               var checkpermission = await _unitOfWork.systempermissionrepository.GetAsync(q=>q.Name==dto.Name);
                if (checkpermission == null)
                {
                    var systemmodule = _mapper.Map<Systempermission>(dto);
                    result = await _unitOfWork.systempermissionrepository.AddAsync(systemmodule);
                    response.Result = systemmodule;



                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Record modification was successful";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = $"{dto.Name} Permission already taken";
                }

            }
            catch (Exception ex)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task SaveModulePermission(int id)
        {
            var submodule = await _unitOfWork.systemsystemsubmodulerepository.GetAsync(q=>q.Id == id,new List<string> { "systemmodule" });
            if(submodule != null)
            {
                var checkpermission = await _unitOfWork.systempermissionrepository.GetAsync(q => q.Name == submodule.AccessName);
                if (checkpermission == null)
                {
                    Systempermission permission = new Systempermission();
                    permission.SystemsubmoduleId = submodule.Id;
                    permission.Name = submodule.AccessName;

                    await _unitOfWork.systempermissionrepository.AddAsync(permission);

                }
                var checkmodulepermission = await _unitOfWork.systempermissionrepository.GetAsync(q => q.Name == submodule.systemmodule.AccessName);
                if (checkmodulepermission == null) {
                    Systempermission permission = new Systempermission();
                    permission.SystemsubmoduleId = submodule.Id;
                    permission.Name = submodule.systemmodule.AccessName;

                    await _unitOfWork.systempermissionrepository.AddAsync(permission);

                }
            }
        }
    }
}
