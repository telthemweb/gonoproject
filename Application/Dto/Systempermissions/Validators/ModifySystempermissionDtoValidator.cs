using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systempermissions.Validators
{
    public class ModifySystempermissionDtoValidator:AbstractValidator<ModifySystempermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModifySystempermissionDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(q => q.Name)
                  .NotEmpty()
                  .WithMessage("{PropertyName} is required");
               
            
        }
    }
}
