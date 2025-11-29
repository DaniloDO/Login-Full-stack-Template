
using LoginApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

using LoginApp.Models; 
using LoginApp.DTOs; 

namespace LoginApp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager; 
    private readonly ITokenService _tokenService; 

    public AuthService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService; 
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
    {
        var existing = await _userManager.FindByEmailAsync(dto.Email);
        if (existing != null)
            throw new InvalidOperationException("Email already in use"); 

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email
        };
        var result = await _userManager.CreateAsync(user, dto.Password);  
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(";", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "User"); 

        var roles = await _userManager.GetRolesAsync(user); 
        var token = _tokenService.CreateToken(user, roles); 

        return new AuthResponseDTO(token, DateTime.UtcNow.AddMinutes(60), user.Id, user.Email);  
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email); 
        if(user is null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var passwordCheck = await _userManager.CheckPasswordAsync(user, dto.Password);
        if(!passwordCheck)
            throw new UnauthorizedAccessException("Invalid credentials"); 

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);  

        return new AuthResponseDTO(token, DateTime.UtcNow.AddMinutes(60), user.Id, user.Email); 
    }
}
