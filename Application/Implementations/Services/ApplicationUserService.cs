using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.Users;
using Application.Logs;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Implementations.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private ConfigurationSettings _settings { get; set; }
        public ApplicationUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IOptions<ConfigurationSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _settings = settings.Value;
        }
        public async Task<BaseResponse> ChangeStatus(string id, string status)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var record = await _unitOfWork.applicationuserrepository.GetAsync(q => q.Id == id);
                if (record == null)
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Record not found";
                }
                else
                {
                    record.Status = status;
                    var result = await _unitOfWork.applicationuserrepository.UpdateAsync(record);
                    if (result > 0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Record successfully modify";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "Record modification failed";
                    }
                }
            }
            catch (Exception ex)
            {

                response.Status=ResponseStatus.ERROR;
                response.Message=ex.Message;

            }
            return response;
        
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var response = await _unitOfWork.applicationuserrepository.GetAllAsync();
            return response;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var response = await _unitOfWork.applicationuserrepository.GetAsync(q=>q.Id == id, new List<string> { "roles.role" });
            return response;
        }

        public async Task<BaseResponse> ModifyUser(ModifyUserDto dto)
        {
            var response = new BaseResponse();
            /*var validator = new ModifyUserDtoValidator(_unitOfWork);
            var validationresult = await validator.ValidateAsync(dto);
            if (validationresult.IsValid == false)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();

            }
            else
            {*/
                if(dto.Id == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.Name = dto.Name;
                    user.Email = dto.Email;
                    user.NormalizedEmail = dto.Email.ToUpper();
                    user.UserName= dto.Email;
                user.NormalizedUserName = dto.Email.ToUpper();
                user.Surname = dto.Surname;
                    user.Title = dto.Title;
                    user.Gender = dto.Gender;
                    user.Status= dto.Status;
                    user.PasswordExpireDate= DateTime.Now.AddDays(-30).ToUniversalTime();
                    var password = await GeneratePassword();
                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                         response.Status = ResponseStatus.SUCCESS;
                         response.Message = "User account successfully created";
                        try
                        {
                            Emailqueue email = new Emailqueue();
                            email.Subject = "New Account Creation";
                            email.Body = $"Good day {user.Name} {user.Surname}  a new account has been created please using the following temporary password  to access  your account: {password}";
                            email.HtmlBody = $"<h5>Good day {user.Name} {user.Surname}</h5>"
                                             + $"<p>A   new account has been created please using the following temporary password  to access  your account: {password} </p>"
                                             + $"<p><a href='{_settings.Url}'>Access Account</a></p>"
                                             + "<p>Please ignore the email if you did not request a password reset</p>";
                            email.Status = "PENDING";
                            email.Recepiants = new List<string> { user.Email };

                            await _unitOfWork.emailqueuerepository.AddAsync(email);
                        }
                        catch (Exception ex)
                        {
                          TelthemwebLogException.LogException(ex);
                        }
                     

                    }
                    else
                    {
                          response.Status= ResponseStatus.ERROR;
                        response.Message = "Failed to  create user";
                    }
                }
                else
                {
                    ApplicationUser user = await _unitOfWork.applicationuserrepository.GetAsync(q=>q.Id==dto.Id);
                    user.Name = dto.Name;
                    user.Email = dto.Email;
                    user.NormalizedEmail = dto.Email.ToUpper();
                    user.Surname = dto.Surname;
                    user.Title = dto.Title;
                    user.Gender = dto.Gender;
                    user.Status = dto.Status;
                    var systemmodule = _mapper.Map(dto, user);
                    var result = await _unitOfWork.applicationuserrepository.UpdateAsync(user);
                    if (result>0)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "User successfully  updated";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "Failed to update record";
                    }
                }
           // }
            return response;
         }

        public async Task<BaseResponse> ResetPassword(string id)
        {
            BaseResponse response = new BaseResponse();
            ApplicationUser user = await _unitOfWork.applicationuserrepository.GetAsync(q => q.Id == id);
            if(user == null)
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "User not found";
            }
            else
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var uuid = Guid.NewGuid();
                Passwordreset passwordreset = new Passwordreset();
                passwordreset.Uuid = uuid.ToString();
                passwordreset.Token = token;
                passwordreset.Email = user.Email;
                passwordreset.ExpireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
                var res = await _unitOfWork.passwordresetrepository.AddAsync(passwordreset);


                if (res > 0)
                {
                    response.Status = ResponseStatus.SUCCESS;
                    response.Message = "Password reset link successfully sent to email";
                    try
                    {
                        Emailqueue email = new Emailqueue();
                        email.Subject = "Password Reset";
                        email.Body = $"Good day {user.Name} {user.Surname}  a  password reset has been requested. Please using the follow link  to reset your password  <a href='{_settings.Url}/forgotpassword/{uuid.ToString()}'>Set New Password</a>. Please ignore the email if you did not request a password reset";
                        email.HtmlBody = $"<h5>Good day {user.Name} {user.Surname}</h5>"
                                         + "<p>A  password reset to your account has been requested.Please using the follow link  to reset your password: </p>"
                                         + $"<p><a href='{_settings.Url}/forgotpassword/{uuid.ToString()}'>Set New Password</a></p>"
                                         + "<p>Please ignore the email if you did not request a password reset</p>";
                        email.Status = "PENDING";
                        email.Recepiants = new List<string> { user.Email };

                        await _unitOfWork.emailqueuerepository.AddAsync(email);

                    }
                    catch (Exception ex)
                    {

                        response.Message = "Account successfully created but email senting failed";
                    }


                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "An error has occurred";
                }
            } 
            return response;
        }

        private async Task<string> GeneratePassword()
        {
             Random rnd = new Random();
             var value = rnd.Next(1000,10000);
            var password = $"Pa22word@{value}";
            return password;
        }

        public async Task<BaseResponse> AssignRole(string userId, string roleId)
        {
           ApplicationUserRole user = new ApplicationUserRole();
            user.UserId = userId;
            user.RoleId = roleId;
            var response = new BaseResponse();
            var result = await _unitOfWork.applicationuserrolerepository.AddAsync(user);
            if (result > 0)
            {
                response.Status = ResponseStatus.SUCCESS;
                response.Message = "Role successfully assigned to user";
            }
            else
            {
                response.Status =ResponseStatus.ERROR;
                response.Message = "Failed to assign role to user";
            }
            return response;
        }

        public async Task<BaseResponse> RemoveRole(string userId, string roleId)
        {
            ApplicationUserRole user =await _unitOfWork.applicationuserrolerepository.GetAsync(q=>q.UserId==userId && q.RoleId==roleId);
            user.UserId = userId;
            user.RoleId = roleId;
            var response = new BaseResponse();
            var result = await _unitOfWork.applicationuserrolerepository.RemoveAsync(user);
            if (result > 0)
            {
                response.Status = ResponseStatus.SUCCESS;
                response.Message = "Role successfully removed from user";
            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Failed to assign role to user";
            }
            return response;
        }
    }
}
