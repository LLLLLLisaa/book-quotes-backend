using BookQuotesBackend.DTOs;
using BookQuotesBackend.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookQuotesBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        bool success = await _authService.Register(request);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Email is already registered."
            });
        }

        return Ok(new
        {
            message = "Registration successful. Please log in."
        });
    }
    
    [HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
{
    bool success = await _authService.Login(request);

    if (!success)
    {
        return Unauthorized(new
        {
            message = "Invalid email or password."
        });
    }

    return Ok(new
    {
        message = "Login successful."
    });
}


    
}