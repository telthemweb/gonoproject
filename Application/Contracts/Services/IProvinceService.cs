using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IProvinceService
    {
        Task<List<Province>> GetAll();
        Task<BaseResponse> Modify(ProvinceModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
