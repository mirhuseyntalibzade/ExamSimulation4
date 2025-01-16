using AutoMapper;
using BL.DTOs.DepartmentDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class DepartmentController : Controller
    {
        readonly IDepartmentService _departmentService;
        readonly IMapper _mapper;
        public DepartmentController(IMapper mapper, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<GetDepartmentDTO> departments = await _departmentService.GetAllDepartments();
                return View(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDepartmentDTO departmentDTO)
        {
            try
            {
                await _departmentService.AddDepartmentAsync(departmentDTO);
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
                GetDepartmentDTO department = await _departmentService.GetDepartmentByIdAsync(Id);
                UpdateDepartmentDTO updateDepartmentDTO = _mapper.Map<UpdateDepartmentDTO>(department);
                return View(updateDepartmentDTO);
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
        public async Task<IActionResult> Update(UpdateDepartmentDTO updateDepartmentDTO)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync(updateDepartmentDTO);
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
                await _departmentService.DeleteDepartmentAsync(Id);
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
