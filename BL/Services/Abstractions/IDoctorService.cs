using BL.DTOs.DoctorDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstractions
{
    public interface IDoctorService
    {
        public Task AddDoctorAsync(AddDoctorDTO doctorDTO);
        public Task UpdateDoctorAsync(UpdateDoctorDTO doctorDTO);
        public Task DeleteDoctorAsync(int Id);
        public Task<GetDoctorDTO> GetDoctorByIdAsync(int Id);
        public Task<ICollection<GetDoctorDTO>> GetAllDoctors();

    }
}
