using Application.Constants;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Data;
using Application.Dto.Identity;
using Application.Dto.Identity.Validators;
using Application.Models;
using Application.Models.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitofwork;
        private ConfigurationSettings _settings { get; set; }
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ConfigurationSettings> settings, IUnitOfWork unitofwork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settings = settings.Value;
            _unitofwork = unitofwork;
        }


        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequestDto request)
        {
            BaseResponse response = new BaseResponse();
            var validator = new ForgetPasswordRequestDtoValidator(_unitofwork);
            var validationresult = await validator.ValidateAsync(request);
            if (validationresult.IsValid)
            {

                var user = await _unitofwork.applicationuserrepository.GetAsync(q => q.Email == request.Email);
                if (user == null)
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Invalid login details";
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var uuid = Guid.NewGuid();
                    Passwordreset passwordreset = new Passwordreset();
                    passwordreset.Uuid = uuid.ToString();
                    passwordreset.Token = token;
                    passwordreset.Email = request.Email;
                    passwordreset.ExpireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
                    var res = await _unitofwork.passwordresetrepository.AddAsync(passwordreset);


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

                            await _unitofwork.emailqueuerepository.AddAsync(email);

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
            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            return response;
        }

       /* public async Task<BaseResponse> Login(AuthRequestDto request)
        {

            BaseResponse response = new BaseResponse();
            try
            {

          
            var validator = new AuthRequestDtoValidator();
            var validationresult = await validator.ValidateAsync(request);
            if (validationresult.IsValid)
            {
                var user = await _unitofwork.applicationuserrepository.GetAsync(q=>q.Email==request.Email);
                if (user == null)
                {
                    response.Status = "ERROR";
                    response.Message = "Invalid login details";
                }
                else
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        response.Status = "ERROR";
                        response.Message = "Incorrect login details";
                    }
                    else
                    {
                        var tokenTimeStamp = DateTime.Now.AddMinutes(double.Parse(_settings.DurationInMinutes));
                         var hasExpired = user.PasswordExpireDate>=DateTime.Now ? "NO" :"YES";
                        AuthResponse authResponse = new AuthResponse();
                        authResponse.Email = request.Email;
                        authResponse.Id = user.Id;
                        authResponse.ExpiryTimeStamp = tokenTimeStamp;
                        authResponse.ExpiresIn = (int)tokenTimeStamp.Subtract(DateTime.Now).TotalSeconds;
                        authResponse.UserName = user.UserName;
                        authResponse.Roles = await GetPermissions(user);
                        authResponse.isExpired = hasExpired;
                        response.Status = "SUCCESS";
                        response.Message = "Successfully logged in";
                        response.Result = authResponse;
                    }
                }
            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            }
            catch (Exception ex)
            {

                response.Status = ResponseStatus.ERROR;
                response.Message = ex.Message;
            }
            return response;
        }
       */

        public async Task<BaseResponse> Register(RegistrationRequestDto request)
        {
            BaseResponse response = new BaseResponse();
            var validator = new RegistrationRequestDtoValidator(_unitofwork);
            var validationresult = await validator.ValidateAsync(request);
            if (validationresult.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.Title = request.Title;
                user.Name = request.FirstName;
                user.Surname = request.LastName;
                user.UserName = request.UserName;
                user.NormalizedEmail = request.Email.ToUpper();
                user.NormalizedUserName = request.UserName.ToUpper();
                user.Status = "ACTIVE";
                user.PasswordExpireDate = DateTime.UtcNow.AddDays(-2);
                user.EmailConfirmed = false;

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    response.Status = ResponseStatus.SUCCESS;
                    response.Message = "Account successfully created";
                    try
                    {

                        Emailqueue email = new Emailqueue();
                        email.Subject = "Account Created";
                        email.Body = $"Good day {user.Name} {user.Surname}  An account to access out portal has been successfully created. Please using the follow link  to activate your account  <a href='{_settings.Url}/activate/{user.Id.ToString()}'>Activate account</a>. Please ignore ths email if you did not initiated account creation";
                        email.HtmlBody = $"<h5>Good day {user.Name} {user.Surname}</h5>"
                                         + "<p>An account to access out portal has been successfully created. Please using the follow link  to activate your account </p>"
                                         + $"<p><a  href='{_settings.Url}/activate/{user.Id.ToString()}'>Activate account</a></p>"
                                         + "<p>Please ignore the email if you did not request a password reset</p>";
                        email.Status = "PENDING";
                        email.Recepiants = new List<string> { user.Email };

                        await _unitofwork.emailqueuerepository.AddAsync(email);
                        await _unitofwork.Save();

                    }
                    catch (Exception ex)
                    {

                        response.Message = "Account successfully created but email senting failed";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = result.Errors.First().Description;
                }

            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            return response;
        }

        public async Task<BaseResponse> ResetPassword(PasswordResetRequestDto request)
        {
            BaseResponse response = new BaseResponse();
            var validator = new PasswordResetRequestDtoValidator(_unitofwork);
            var validationresult = await validator.ValidateAsync(request);
            if (validationresult.IsValid)
            {
                var getrecord = await _unitofwork.passwordresetrepository.GetAsync(q => q.Uuid == request.Token);
                if (getrecord.Email == request.Email)
                {
                    var user = await _unitofwork.applicationuserrepository.GetAsync(q => q.Email == request.Email);
                    var resetresponse = await _userManager.ResetPasswordAsync(user, getrecord.Token, request.ConfirmPassword);
                    if (resetresponse.Succeeded)
                    {
                        response.Status = ResponseStatus.SUCCESS;
                        response.Message = "Password successfully reset";
                    }
                    else
                    {
                        response.Status = ResponseStatus.ERROR;
                        response.Message = "Password reset failed";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Records mismatch";
                }
            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            return response;
        }

      

        public async Task<BaseResponse> ActivateAccount(string userid)
        {
            BaseResponse response = new BaseResponse();
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                user.EmailConfirmed = true;
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    response.Status = ResponseStatus.SUCCESS;
                    response.Message = "Password successfully reset";
                }
                else
                {
                    response.Status = ResponseStatus.ERROR;
                    response.Message = "Failed to activate account";
                }
            }
            else
            {
                response.Status = ResponseStatus.ERROR;
                response.Message = "Validation Errors";
            }
            return response;
        }

        public async Task<AuthResponse> GetPermissions(ApplicationUser user)
        {
            var response = await _unitofwork.applicationuserrolerepository.GetWithPermissions(user.Id);
            List<string> permissions = new List<string>();
            if (response != null)
            {
                permissions = response.ToList().SelectMany(q => q.role.assignedpermissions.Select(q => q.permission.Name)).ToList();
            }

            AuthResponse authResponse = new AuthResponse();
            var hasExpired = user.PasswordExpireDate >= DateTime.Now ? "NO" : "YES";
            var tokenTimeStamp = DateTime.Now.AddMinutes(double.Parse(_settings.DurationInMinutes));
            authResponse.Email = user.Email;
            authResponse.Id = user.Id;
            authResponse.ExpiryTimeStamp = tokenTimeStamp;
            authResponse.ExpiresIn = (int)tokenTimeStamp.Subtract(DateTime.Now).TotalSeconds;
            authResponse.UserName = user.UserName;
            authResponse.Roles = permissions;
            authResponse.isExpired = hasExpired;
            return authResponse;
        }
    }
}
