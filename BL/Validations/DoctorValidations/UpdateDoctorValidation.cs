﻿using BL.DTOs.DoctorDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validations.DoctorValidations
{
    public class UpdateDoctorValidation : AbstractValidator<UpdateDoctorDTO>
    {
        public UpdateDoctorValidation()
        {
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DepartmentId).NotEmpty();
        }
    }
}
