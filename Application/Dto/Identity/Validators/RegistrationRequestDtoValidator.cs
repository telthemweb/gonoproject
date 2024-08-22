using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Identity.Validators
{
     public class RegistrationRequestDtoValidator:AbstractValidator<RegistrationRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegistrationRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(q => q.Email)
                    .NotEmpty()
                    .WithMessage("{PropertyName} cannot be empty")
                    .EmailAddress()
                    .WithMessage("{PropertyName} should be avalid email")
                    .MustAsync(async (Email, token) =>
                    {
                        var checkemail = await _unitOfWork.applicationuserrepository.GetAsync(q => q.Email == Email);
                        if (checkemail != null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }).WithMessage("{PropertyName} already taken");
            RuleFor(q => q.Password)
                 .NotEmpty()
                 .WithMessage("{PropertyName} cannot be empty")
                 .MinimumLength(6)
                 .WithMessage("{PropertyName} should not be less than 6 characters");
            RuleFor(q => q.LastName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            RuleFor(q => q.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            RuleFor(q => q.UserName)
             .NotEmpty()
             .WithMessage("{PropertyName} cannot be empty")
             .EmailAddress()
             .WithMessage("{PropertyName} should be avalid email")
             .MustAsync(async (UserName, token) =>
             {
                 var checkemail = await _unitOfWork.applicationuserrepository.GetAsync(q => q.UserName == UserName);
                 if (checkemail != null)
                 {
                     return false;
                 }
                 else
                 {
                     return true;
                 }
             }).WithMessage("{PropertyName} already taken");

        }
    }
}
