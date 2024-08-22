using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IVisitorLoggerService
    {
        Task<BaseResponse> CheckInVisitor(VisitorLoggerModifyDto dto);
        Task<BaseResponse> CheckOutVisitor(VisitorLoggerModifyDto dto);
    }
}
