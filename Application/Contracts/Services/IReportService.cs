using Application.Data;

namespace Application.Contracts.Services
{
    public interface IReportService
    {
        Task<List<Visitor>> GetVisitorReports(DateTime? fromdateTime,DateTime? todateTime);
        Task<List<Systemlog>> GetVisitorlogReports(DateTime? fromdateTime, DateTime? todateTime);
    }
}
