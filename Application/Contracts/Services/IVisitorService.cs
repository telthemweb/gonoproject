using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IVisitorService
    {
        Task<List<Visitor>> GetAll();
        Task<BaseResponse> Modify(VisitorModifyDto dto);
        Task<Visitor> GetOne(int id);
        Task<BaseResponse> Delete(int id);
    }
}
