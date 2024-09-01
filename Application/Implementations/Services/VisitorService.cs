using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto;
using Application.Models;
using AutoMapper;
using System.Globalization;

namespace Application.Implementations.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitorService(IUnitOfWork unitOfWork, IMapper mapper = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.visitorRepository.GetAsync(q => q.Id == id);
                if (record != null)
                {
                    var result = await _unitOfWork.visitorRepository.RemoveAsync(record);

                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = CommonPhrases.DELETE_MESSAGE;
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = CommonPhrases.EXCEPTIONERROR;
                    }
                }

            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<List<Visitor>> GetAll()
        {
            try
            {
                var response = await _unitOfWork.visitorRepository.GetAllAsync(new List<string>
                    {
                       "Title",
                        "Nationality",
                        "Province",
                        "Gender",
                        "City",
                        "MaritalStatus"
                    });
                return response.ToList();
            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                throw;
            }
        }

      

        public async Task<Visitor> GetOne(int id)
        {
            try
            {
                var response = await _unitOfWork.visitorRepository.GetAsync(q => q.Id == id, new List<string> {
                                                                                                "visitorlog",
                                                                                                "Title",
                                                                                                "Nationality",
                                                                                                "Province",
                                                                                                "Gender",
                                                                                                "City",
                                                                                                "MaritalStatus"
                                                                                                });
                return response;
            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                throw;
            }
        }

        public async Task<BaseResponse> Modify(VisitorModifyDto dto)
        {
            var response = new BaseResponse();
            var responsed = await _unitOfWork.visitornumberRepository.GetAsync(q => q.Year == DateTime.Now.Year);
            var nextCertificateNumber = responsed != null ? responsed.LastNumber + 1 : 1;
            int num = 0;
            num = (int)nextCertificateNumber!;
            var resultd = responsed!.Prefix + num.ToString("D6");

            responsed.LastNumber=num;
            await _unitOfWork.visitornumberRepository.UpdateAsync(responsed);
            try
            {
                int result = 0;
                if (dto.Id != null)
                {
                    Visitor record = await _unitOfWork.visitorRepository.GetAsync(q => q.Id == dto.Id);
                    var visitor = _mapper.Map(dto, record);
                    result = await _unitOfWork.visitorRepository.UpdateAsync(visitor);
                    response.Result = visitor;
                }
                else
                {
                    var data = _mapper.Map<Visitor>(dto);
                    data.FullName = dto.FirstName + " " + dto.PreviousName + " " + dto.LastName;
                    data.Profile = "profile/placeholder.png";
                    data.Age = CalculateAge(dto.Dob!);
                    data.VisitorNumber = resultd;
                    result = await _unitOfWork.visitorRepository.AddAsync(data);
                    response.Result = data;
                }

                if (result > 0)
                {
                    response.Status = ResponseStatus.SUCCESS;
                    response.Message = CommonPhrases.SUCCESS_CREATE;
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = CommonPhrases.EXCEPTIONERROR;
                }

            }
            catch (Exception ex)
            {
                TelthemwebLogException.LogException(ex);
                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }
            return response;
        }

        private int CalculateAge(string dateOfBirthStr)
        {
            string[] formats = {
            "dd/MM/yy", "yyyy-MM-dd", "dd-MMMM-yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "dd/MM/yyyy" // list of formats
        };

            DateTime dateOfBirth;
            if (!DateTime.TryParseExact(dateOfBirthStr.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                throw new FormatException("Invalid date format.");
            }

            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
