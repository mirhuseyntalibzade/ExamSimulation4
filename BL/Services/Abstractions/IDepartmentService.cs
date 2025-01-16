using BL.DTOs.DepartmentDTOs;
using CORE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstractions
{
    public interface IDepartmentService
    {
        public Task<ICollection<GetDepartmentDTO>> GetAllDepartments();
        public Task<GetDepartmentDTO> GetDepartmentByIdAsync(int Id);
        public Task AddDepartmentAsync(AddDepartmentDTO departmentDTO);
        public Task UpdateDepartmentAsync(UpdateDepartmentDTO departmentDTO);
        public Task DeleteDepartmentAsync(int Id);
        public Task<ICollection<SelectListItem>> SelectDepartmentsAsync();
    }
}
