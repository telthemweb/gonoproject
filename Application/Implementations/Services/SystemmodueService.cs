using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.Systemmodules;
using Application.Dto.Systemmodules.Validators;
using Application.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class SystemmodueService : ISystemmoduleService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SystemmodueService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.systemmodulerepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                var result=    await _unitOfWork.systemmodulerepository.RemoveAsync(record);
                
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

                response.Status=ResponseStatus.ERROR;
                response.Message=ex.Message;
            }
      
            return response;
        }

        public async Task<List<Systemmodule>> GetAll()
        {
            try
            {
                var response = await _unitOfWork.systemmodulerepository.GetAllAsync(new List<string> { "submodules.systempermissions" });
                return response.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<List<Systemmodule>> GetByRoleId(string roleId)
        {
            var response = await _unitOfWork.systemmodulerepository.GetModulesByRole(roleId);
            return response.ToList();
        }

        public async Task<List<Systemmodule>> GetCached()
        {
            return await _unitOfWork.systemmodulerepository.GetCachedAll();
        }

        public async Task<BaseResponse> Modify(ModifySystemmoduleDto dto)
        {
            var response = new BaseResponse();
            var validator = new ModifySystemmoduleDtoValidator(_unitOfWork);
            var validationresult = await validator.ValidateAsync(dto);
            if (validationresult.IsValid == false)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();

            }
            else
            {
                try
                {
                    int result = 0;
                    if (dto.Id != null)
                    {
                        Systemmodule record = await _unitOfWork.systemmodulerepository.GetAsync(q => q.Id == dto.Id);
                        var systemmodule = _mapper.Map(dto,record);
                       result= await _unitOfWork.systemmodulerepository.UpdateAsync(systemmodule);
                        response.Result = systemmodule;
                    }
                    else
                    {
                        var systemmodule = _mapper.Map<Systemmodule>(dto);
                        result=await _unitOfWork.systemmodulerepository.AddAsync(systemmodule);
                        response.Result = systemmodule;
                    }
                    
                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Record modification was successful";
                    }
                   
                }
                catch (Exception ex)
                {

                    response.Status = ResponseStatus.ERROR;
                    response.Message = ex.Message;
                }
            
            }
            return response;
        }
    }
}
