using AutoMapper;
using BL.DTOs.DoctorDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Areas.Admin.Controllers
{
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
                await _doctorService.GetAllDoctors();
                return View();
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
                return View("Index");
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

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDoctorDTO updateDoctorDTO)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(updateDoctorDTO);
                return View("Index");
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
                Doctor doctor = await _doctorService.GetDoctorByIdAsync(Id);
                UpdateDoctorDTO updateDoctorDTO = _mapper.Map<UpdateDoctorDTO>(doctor);
                return View(doctor);
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

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _doctorService.DeleteDoctorAsync(Id);
                return RedirectToAction("Home", "Index");
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
