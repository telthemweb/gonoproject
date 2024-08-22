using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.Roles;
using Application.Dto.Roles.Validators;
using Application.Dto.Systemmodules.Validators;
using Application.Implementations.Repositories;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;
        private RoleManager<Role> _roleManager;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper = null, RoleManager<Role> roleManager = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<BaseResponse> Delete(string id)
        {

            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.rolerepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.rolerepository.RemoveAsync(record);

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

        public async Task<List<Role>> GetAll()
        {
            var response = await _unitOfWork.rolerepository.GetAllAsync();
            return response;
          
        }

        public async Task<Role> GetById(string id)
        {
           var response = await _unitOfWork.rolerepository.GetAsync(q=>q.Id == id, new List<string> { "assignedpermissions" });
            return response;
        }

        public async Task<List<Role>> GetRoleByUserId(string userId)
        {
            var response = await _roleManager.Roles.Include(q => q.applicationuserroles.Where(q => q.UserId == userId)).ToListAsync();
            return response;
        }

        public async Task<BaseResponse> Modify(ModifyRoleDto dto)
        {
            var response = new BaseResponse();
            var validator = new ModifyRoleDtoValidator();
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

                        Role record = await _roleManager.FindByIdAsync(dto.Id);
                        record.Name = dto.Name;
                        record.NormalizedName = dto.Name.Normalize();
                        var updateresult = await  _roleManager.UpdateAsync(record);
                        if (updateresult.Succeeded)
                        {
                            result = 1;
                        }
                        response.Result = record;
                    }
                    else
                    {
                        Role record = new Role();
                        record.Name = dto.Name;
                        record.NormalizedName = dto.Name.Normalize();
                        var updateresult = await _roleManager.CreateAsync(record);
                        if (updateresult.Succeeded)
                        {
                            result = 1;
                        }
                        response.Result = record;
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
