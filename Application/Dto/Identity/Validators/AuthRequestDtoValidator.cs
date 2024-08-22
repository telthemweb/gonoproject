using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Identity.Validators
{
    public class AuthRequestDtoValidator:AbstractValidator<AuthRequestDto>
    {
        public AuthRequestDtoValidator()
        {
            RuleFor(q=>q.Email)
                .NotEmpty()
                .WithMessage("{PropertyName}  cannot be empty")
                .EmailAddress()
                .WithMessage("{PropertyName} should be a valid email");
            RuleFor(q => q.Password)
                 .NotEmpty()
                 .WithMessage("{PropertyName} password cannot be empty")
                 .MinimumLength(6)
                 .WithMessage("{PropertyName} password should be atleast 6 characters");
        }
    }
}
