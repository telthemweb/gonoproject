using Application.Data;
using Application.Dto.Identity;
using Application.Models;
using Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IAuthService
    {
        //Task<BaseResponse> Login(AuthRequestDto request);

        Task<BaseResponse> Register(RegistrationRequestDto request);

        Task<BaseResponse> ForgotPassword(ForgotPasswordRequestDto request);

        Task<BaseResponse> ResetPassword(PasswordResetRequestDto request);

        Task<BaseResponse> ActivateAccount(string userid);

        Task<AuthResponse> GetPermissions(ApplicationUser user);
    }
}
