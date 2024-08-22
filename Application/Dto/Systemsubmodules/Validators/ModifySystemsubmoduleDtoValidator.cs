using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemsubmodules.Validators
{
    public class ModifySystemsubmoduleDtoValidator:AbstractValidator<ModifySystemsubmoduleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModifySystemsubmoduleDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(q => q.Name)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (Name, token) =>
                {
                    var check = await _unitOfWork.systemsystemsubmodulerepository.GetAsync(q => q.Name.ToLower() == Name.ToLower());
                    return check != null ? false : true;
                }).WithMessage("{PropertyName} Name already taken");
        }
    }
}
