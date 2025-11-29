using LoginApp.DTOs;
using LoginApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDTO dto)
    {
        try
        {
            var res = await _authService.RegisterAsync(dto);
            return Ok(res);
        }

        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO dto)
    {
        try
        {
            var res = await _authService.LoginAsync(dto);
            return Ok(res);
        }

        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }

        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("access")]
    [Authorize]
    public ActionResult VerifyAccess()
    {
        return Ok("Regular access");
    }

    [HttpGet("user-access")]
    [Authorize(Roles = "User")]
    public ActionResult VerifyUserAccess()
    {
        return Ok("User access");
    }

    [HttpGet("admin-access")]
    [Authorize(Roles = "Admin")]
    public ActionResult VerifyAdminAccess()
    {
        return Ok("Admin access");
    }

}
