using BL.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstractions
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterDTO useDTO);
        public Task LoginAsync(LoginDTO userDTO);
        public Task LogoutAsync();
    }
}
