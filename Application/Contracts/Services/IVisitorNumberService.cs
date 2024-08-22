using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IVisitorNumberService
    {
        Task<List<VisitorNumber>> GetAll();
        Task<BaseResponse> Modify(VisitorNumberModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
