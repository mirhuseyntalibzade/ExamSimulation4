using BL.DTOs.DepartmentDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validations.DepartmentValidations
{
    public class AddDepartmentValidation : AbstractValidator<AddDepartmentDTO>
    {
        public AddDepartmentValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
