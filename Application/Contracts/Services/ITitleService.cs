using Application.Data;
using Application.Dto;
using Application.Models;

namespace Application.Contracts.Services
{
    public interface ITitleService
    {
        Task<List<Title>> GetAll();
        Task<BaseResponse> Modify(TitleModifyDto dto);
        Task<BaseResponse> Delete(int id);
    }
}
