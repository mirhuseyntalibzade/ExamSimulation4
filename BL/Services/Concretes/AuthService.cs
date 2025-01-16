using AutoMapper;
using BL.DTOs.AuthDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class AuthService : IAuthService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task LoginAsync(LoginDTO userDTO)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user is null)
            {
                throw new CredentialException("Credentials are not correct.");
            }
            bool isSucceed= await _userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!isSucceed)
            {
                throw new CredentialException("Credentials are not correct.");
            }
            await _signInManager.SignInAsync(user,true);
        }


        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Username,
            };
            IdentityResult userResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!userResult.Succeeded)
            {
                throw new OperationNotValidException("Couldn't create user.");
            }
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                throw new OperationNotValidException("Couldn't create user.");
            }

        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
