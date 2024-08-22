using Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Systemmodules.Validators
{
    public class ModifySystemmoduleDtoValidator:AbstractValidator<ModifySystemmoduleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModifySystemmoduleDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(q => q.Name)
                 .NotEmpty()
                 .WithMessage("{PropertyName} required")
                 .MustAsync(async (Name, token) =>
                 {
                   var checkname = await _unitOfWork.systemmodulerepository.GetAsync(q=>q.Name.ToLower()==Name.ToLower());
                    if (checkname != null)
                     {
                         return false;
                     }else
                     {
                         return true;
                     }
                 }).WithMessage("{PropertyName} already taken");
        }
    }
}
