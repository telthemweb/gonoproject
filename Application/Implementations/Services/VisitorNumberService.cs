using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto;
using Application.Models;
using AutoMapper;

namespace Application.Implementations.Services
{
    public class VisitorNumberService : IVisitorNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitorNumberService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.visitornumberRepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.visitornumberRepository.RemoveAsync(record);

                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = CommonPhrases.DELETE_MESSAGE;
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = CommonPhrases.EXCEPTIONERROR;
                    }
                }

            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<List<VisitorNumber>> GetAll()
        {
            try
            {
                var response = await _unitOfWork.visitornumberRepository.GetAllAsync();
                return response.ToList();
            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                throw;
            }
        }

        public async Task<BaseResponse> Modify(VisitorNumberModifyDto dto)
        {
            var response = new BaseResponse();

            try
            {
                int result = 0;
                if (dto.Id != null)
                {
                    VisitorNumber record = await _unitOfWork.visitornumberRepository.GetAsync(q => q.Id == dto.Id);
                    var visitor = _mapper.Map(dto, record);
                    result = await _unitOfWork.visitornumberRepository.UpdateAsync(visitor);
                    response.Result = visitor;
                }
                else
                {
                    var data = _mapper.Map<VisitorNumber>(dto);
                    result = await _unitOfWork.visitornumberRepository.AddAsync(data);
                    response.Result = data;
                }

                if (result > 0)
                {
                    response.Status = ResponseStatus.SUCCESS;
                    response.Message = CommonPhrases.SUCCESS_CREATE;
                }
                else {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = CommonPhrases.EXCEPTIONERROR;
                }

            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
