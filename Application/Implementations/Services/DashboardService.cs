using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;

namespace Application.Implementations.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IUnitOfWork _unitOfWork;
        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<VisitorLogger>> GetDashboardVisitorLogger()
        {
            var respone = await _unitOfWork.visitorloggerRepository.GetAllAsync(
                new List<string> {
                    "visitor.Gender",
                    "visitor.City",
                });
            return respone.Where(q => q.Status == "PENDING").OrderByDescending(q => q.DateCreated).ToList();
        }

        public async Task<int> GetDashboardVisitorsTotal()
        {
            List<Visitor> visitorlist = await _unitOfWork.visitorRepository.GetAllAsync();
            return visitorlist.Count;
        }

        public async Task<int> GetDashboardVisitorTotallogs()
        {
            List<VisitorLogger> logs = await _unitOfWork.visitorloggerRepository.GetAllAsync();
            var logcounts= logs.Where(q => q.Status == "PENDING").ToList();
            return logcounts.Count;
        }
    }
}
