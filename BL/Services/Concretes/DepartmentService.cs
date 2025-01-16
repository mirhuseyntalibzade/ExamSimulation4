using AutoMapper;
using BL.DTOs.DepartmentDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using DAL.Repository.Absractions;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<ICollection<Department>> GetAllDepartments()
        {
            ICollection<Department> departments = await _departmentRepository.GetAllAsync();
            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int Id)
        {
            Department department = await _departmentRepository.GetByIdAsync(Id);
            if (department is null)
            {
                throw new ItemNotFoundException("Couldnt find item.");
            }
            return department;
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
            _departmentRepository.Update(department);
            int result = await _departmentRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't update department.");
            }
        }
    }
}
