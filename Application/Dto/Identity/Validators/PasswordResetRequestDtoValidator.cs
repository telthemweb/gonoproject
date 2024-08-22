using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Identity.Validators
{
    public class PasswordResetRequestDtoValidator:AbstractValidator<PasswordResetRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PasswordResetRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            RuleFor(q => q.Email)
                .NotNull()
                .WithMessage("{PropertyName} email cannot be empty")
                .EmailAddress()
                .WithMessage("{ProperyName} has to be valid email");
            RuleFor(q => q.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty")
                .MinimumLength(6)
                .WithMessage("{PropertyName} should be atleast 6 characters");
            RuleFor(q => q.Token)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty")
                .MustAsync(async (Token,token) =>
                {
                    var checktoken = await _unitOfWork.passwordresetrepository.GetAsync(x=>x.Uuid==Token);
                    if (checktoken == null)
                    {
                        return false;
                    }
                    else if(checktoken.ExpireDate<DateOnly.FromDateTime(DateTime.Now)) {
                      return false;
                    }
                   return true;
                    
                }).WithMessage("{PropertyName} Token not found or Expired");
                

        }
    }
}
