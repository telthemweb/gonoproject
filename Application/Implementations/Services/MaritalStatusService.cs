using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto;
using Application.Models;
using AutoMapper;

namespace Application.Implementations.Services
{
    public class MaritalStatusService : IMaritalStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaritalStatusService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.maritalstatusrepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.maritalstatusrepository.RemoveAsync(record);

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

        public async Task<List<MaritalStatus>> GetAll()
        {
            try
            {
                var response = await _unitOfWork.maritalstatusrepository.GetAllAsync();
                return response.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<BaseResponse> Modify(MaritalStatusModifyDto dto)
        {
            var response = new BaseResponse();

            try
            {
                int result = 0;
                if (dto.Id != null)
                {
                    MaritalStatus record = await _unitOfWork.maritalstatusrepository.GetAsync(q => q.Id == dto.Id);
                    var MaritalStatus = _mapper.Map(dto, record);
                    result = await _unitOfWork.maritalstatusrepository.UpdateAsync(MaritalStatus);
                    response.Result = MaritalStatus;
                }
                else
                {
                    var data = _mapper.Map<MaritalStatus>(dto);
                    result = await _unitOfWork.maritalstatusrepository.AddAsync(data);
                    response.Result = data;
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
            return response;
        }
    }
}
