using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Roles.Validators
{
    public class ModifyRoleDtoValidator : AbstractValidator<ModifyRoleDto>
    {
        public ModifyRoleDtoValidator()
        {
            RuleFor(q => q.Name).NotEmpty()
                              .WithMessage("{PropertyName} is required")
                              .NotNull()
                              .WithMessage("{PropertyName} cannot be null");
        }
    }
}
