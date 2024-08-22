using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface IGenderService
    {
        Task<List<Gender>> GetAll();
        Task<BaseResponse> Modify(GenderModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
