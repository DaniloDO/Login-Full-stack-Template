using System;
using LoginApp.DTOs; 

namespace LoginApp.Services.Interfaces;

public interface IAuthService
{
    public Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto); 
    public Task<AuthResponseDTO> LoginAsync(LoginDTO dto); 
}
