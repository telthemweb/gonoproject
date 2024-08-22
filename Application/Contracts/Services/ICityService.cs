using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAll();
        Task<BaseResponse> Modify(CityModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
