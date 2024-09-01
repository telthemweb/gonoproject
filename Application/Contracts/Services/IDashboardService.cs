using Application.Data;

namespace Application.Contracts.Services
{
    public interface IDashboardService
    {
        Task<int> GetDashboardVisitorsTotal();
        Task<int> GetDashboardVisitorTotallogs();
        Task<List<VisitorLogger>> GetDashboardVisitorLogger();
    }
}
