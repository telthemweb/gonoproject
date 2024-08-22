using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto;
using Application.Models;
using AutoMapper;

namespace Application.Implementations.Services
{
    public class VisitorLoggerService : IVisitorLoggerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitorLoggerService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> CheckInVisitor(VisitorLoggerModifyDto dto)
        {
            var response = new BaseResponse();
            try
            {
                int result = 0;
                var currentDate = DateTime.Now;
                var checkvisitor = await _unitOfWork.visitorloggerRepository.GetAsync(q => q.VisitorId == dto.VisitorId && q.DateCheckin.HasValue && q.DateCheckin.Value.Date== currentDate.Date);
                if (checkvisitor != null)
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Visitor is already check in!!";
                }
                else
                {
                    var currentTime = DateTime.Now;
                    var checkin= currentTime.Hour +":"+ currentTime.Minute+":"+ currentTime.Second;
                    var data = _mapper.Map<VisitorLogger>(dto);
                    data.TimeCheckout = "";
                    data.DateCheckin = currentDate.Date;
                    data.TimeCheckin = checkin;
                    data.Status = "PENDING";
                    data.Purposeofvisit =dto.Purposeofvisit;
                    result = await _unitOfWork.visitorloggerRepository.AddAsync(data);
                    response.Result = data;
                    if (result > 0)
                    {
                        Systemlog log = new Systemlog();
                        log.VisitorId = dto.VisitorId;
                        log.DateCheckin = currentDate.Date;
                        log.TimeCheckin = checkin;
                        log.TimeCheckout = "";
                        log.Status = "CHECKIN";
                        await _unitOfWork.systemlogrepository.AddAsync(log);
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Visitor checked in Successfully";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "Error occured trying to Visitor checked in";
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

        public async Task<BaseResponse> CheckOutVisitor(VisitorLoggerModifyDto dto)
        {
            var response = new BaseResponse();
            try
            {
                int result = 0;
                var currentDate = DateTime.Now;
                var checkvisitor = await _unitOfWork.visitorloggerRepository.GetAsync(q => q.VisitorId == dto.VisitorId && q.DateCheckin.HasValue && q.DateCheckin.Value.Date == currentDate.Date);
                if (checkvisitor == null)
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Visitor is not found!!";
                }
                else
                {
                    var currentTime = DateTime.Now;
                    var checkout = currentTime.Hour + ":" + currentTime.Minute + ":" + currentTime.Second;
                    var data = _mapper.Map<VisitorLogger>(dto);
                    data.TimeCheckout = checkout;
                    data.Status = "CHECKOUT";
                    result = await _unitOfWork.visitorloggerRepository.AddAsync(data);
                    response.Result = data;
                    if (result > 0)
                    {
                        Systemlog log = new Systemlog();
                        log.VisitorId = dto.VisitorId;
                        log.DateCheckin = checkvisitor.DateCheckin;
                        log.TimeCheckin = checkvisitor.TimeCheckin;
                        log.TimeCheckout = checkout;
                        log.Status = "CHECKOUT";
                        await _unitOfWork.systemlogrepository.AddAsync(log);

                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Visitor checked OUT Successfully";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "Error occured trying to Visitor checked in";
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
    }
}
