using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;

namespace Application.Implementations.Services
{

    public class ReportService : IReportService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Systemlog>> GetVisitorlogReports(DateTime? fromdateTime, DateTime? todateTime)
        {
            try
            {
                var fromdate = fromdateTime!.Value.Date;
                var todate = todateTime!.Value.Date;
                var response = await _unitOfWork.systemlogrepository.GetAllAsync(new List<string> {
                    "visitor.Gender",
                    "visitor.City",
                });
                var datalist = response.ToList();
                var visitorlogs = datalist.Where(q => q.DateCheckin!.Value.Date >= fromdate && q.DateCheckin.Value.Date <= todate);
                return visitorlogs.ToList();
            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                throw;
            }
        }

        public async Task<List<Visitor>> GetVisitorReports(DateTime? fromdateTime, DateTime? todateTime)
        {
            try
            {
                var fromdate = fromdateTime!.Value.Date;
                var todate = todateTime!.Value.Date;
                var response = await _unitOfWork.visitorRepository.GetAllAsync(new List<string> {
                    "Gender",
                    "City",
                    "MaritalStatus",
                     "Title",
                      "Province",
                       "Nationality",
                });
                var datalist = response.ToList();
                var visitorlogs = datalist.Where(q => q.DateCreated.Date >= fromdate && q.DateCreated.Date <= todate);
                return visitorlogs.ToList();
            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                throw;
            }
        }
    }
}
