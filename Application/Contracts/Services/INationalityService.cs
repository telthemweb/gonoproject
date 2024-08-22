using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface INationalityService
    {
        Task<List<Nationality>> GetAll();
        Task<BaseResponse> Modify(NationalityModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
