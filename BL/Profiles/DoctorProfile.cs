using AutoMapper;
using BL.DTOs.DoctorDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, GetDoctorDTO>().ReverseMap();
            CreateMap<Doctor, AddDoctorDTO>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDTO>().ReverseMap();
            CreateMap<GetDoctorDTO, UpdateDoctorDTO>().ReverseMap();

        }
    }
}
