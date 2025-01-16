using AutoMapper;
using BL.DTOs.DepartmentDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using DAL.Repository.Absractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BL.Services.Concretes
{
    public class DepartmentService : IDepartmentService
    {
        readonly IDepartmentRepository _departmentRepository;
        readonly IMapper _mapper;
        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task AddDepartmentAsync(AddDepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<Department>(departmentDTO);
            await _departmentRepository.AddAsync(department);
            int result = await _departmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't add department.");
            }
        }

        public async Task DeleteDepartmentAsync(int Id)
        {
            Department department = await _departmentRepository.GetByIdAsync(Id);
            if (department is null)
            {
                throw new ItemNotFoundException("Couldnt find item.");
            }
            _departmentRepository.Delete(department);
            int result = await _departmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't delete department.");
            }
        }

        public async Task<ICollection<GetDepartmentDTO>> GetAllDepartments()
        {
            ICollection<Department> departments = await _departmentRepository.GetAllAsync();
            ICollection<GetDepartmentDTO> departmentDTOs = _mapper.Map<ICollection<GetDepartmentDTO>>(departments);
            return departmentDTOs;
        }

        public async Task<GetDepartmentDTO> GetDepartmentByIdAsync(int Id)
        {
            Department department = await _departmentRepository.GetByIdAsync(Id);
            if (department is null)
            {
                throw new ItemNotFoundException("Couldnt find item.");
            }
            GetDepartmentDTO departmentDTO = _mapper.Map<GetDepartmentDTO>(department);
            return departmentDTO;
        }

        public async Task<ICollection<SelectListItem>> SelectDepartmentsAsync()
        {
           return await _departmentRepository.SelectDepartmentsAsync();
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDTO departmentDTO)
        {
            Department department = await _departmentRepository.GetByConditionAsync(d => d.Id == departmentDTO.Id);
            if (department is null)
            {
                throw new ItemNotFoundException("Couldnt find item.");
            }
            Department updatedDepartment = _mapper.Map<Department>(departmentDTO);
            _departmentRepository.Update(updatedDepartment);
            int result = await _departmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't update department.");
            }
        }
    }
}
