using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IMaritalStatusService
    {
        Task<List<MaritalStatus>> GetAll();
        Task<BaseResponse> Modify(MaritalStatusModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
