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

        Console.WriteLine(success);
        if (!success)
        {
            return BadRequest("Email is already registered.");
        }

        return Ok();
    }


    
}