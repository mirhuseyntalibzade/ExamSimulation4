using BL.DTOs.AuthDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using BL.Validations.AuthValidations;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var validator = new LoginValidation();
                var results = validator.Validate(loginDTO);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        ModelState.AddModelError(failure.ErrorCode, failure.ErrorMessage);
                    }
                    return View();
                }

                await _authService.LoginAsync(loginDTO);
                return RedirectToAction("Index","Home");
            }
            catch(CredentialException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                var validator = new RegisterValidation();
                var results = validator.Validate(registerDTO);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        ModelState.AddModelError(failure.ErrorCode, failure.ErrorMessage);
                    }
                    return View();
                }
                await _authService.RegisterAsync(registerDTO);
                return RedirectToAction("Login","Account");
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
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
