using Application.Contracts.Persistence;
using Application.Dto.Systemsubmodules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Users.Validators
{
    public class ModifyUserDtoValidator: AbstractValidator<ModifyUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModifyUserDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;         
         When(q => q.Id == null, () =>
         {
             RuleFor(q => q.Email)
            .MustAsync(async (Email, token) =>
            {
                var check = await _unitOfWork.applicationuserrepository.GetAsync(q => q.Email.ToLower() == Email.ToLower());
                
                return check != null ? false : true;
            }).WithMessage("{PropertyName}  already taken");
         });
        
           
        }
    
    }
}
