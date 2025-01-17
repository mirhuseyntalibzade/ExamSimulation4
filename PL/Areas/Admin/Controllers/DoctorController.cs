using AutoMapper;
using BL.DTOs.DoctorDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class DoctorController : Controller
    {
        readonly IDoctorService _doctorService;
        readonly IDepartmentService _departmentService;
        readonly IMapper _mapper;
        public DoctorController(IDepartmentService departmentService, IMapper mapper, IDoctorService doctorService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<GetDoctorDTO> doctors = await _doctorService.GetAllDoctors();
                return View(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                ICollection<SelectListItem> departments = await _departmentService.SelectDepartmentsAsync();
                AddDoctorDTO doctorDTO = new AddDoctorDTO
                {
                    Departments = departments
                };
                return View(doctorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDoctorDTO doctorDTO)
        {
            try
            {
                await _doctorService.AddDoctorAsync(doctorDTO);
                return RedirectToAction("Index");
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> Update(int Id)
        {
            try
            {
                GetDoctorDTO doctorDTO = await _doctorService.GetDoctorByIdAsync(Id);
                UpdateDoctorDTO updateDoctorDTO = _mapper.Map<UpdateDoctorDTO>(doctorDTO);
                ICollection<SelectListItem> departments = await _departmentService.SelectDepartmentsAsync();
                updateDoctorDTO.Departments = departments;
                return View(updateDoctorDTO);
            }
            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDoctorDTO updateDoctorDTO)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(updateDoctorDTO);
                return RedirectToAction("Index");
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _doctorService.DeleteDoctorAsync(Id);
                return RedirectToAction("Index");
            }
            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
