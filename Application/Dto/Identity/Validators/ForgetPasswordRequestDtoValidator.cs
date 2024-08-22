using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Identity.Validators
{
    public class ForgetPasswordRequestDtoValidator:AbstractValidator<ForgotPasswordRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ForgetPasswordRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(q => q.Email)
                 .NotEmpty()
                 .WithMessage("{PropertyName} is required")
                 .EmailAddress()
                 .WithMessage("{PropertyName} should be a valid email")
                 .MustAsync(async (Email, token) =>
                 {
                     var user = await _unitOfWork.applicationuserrepository.GetAsync(q => q.Email.ToLower() == Email.ToLower());
                     return user == null ? false : true;
                 }).WithMessage("{PropertyName} doesnt exist");
        }
    }
}
