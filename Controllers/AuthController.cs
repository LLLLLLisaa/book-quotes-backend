using BookQuotesBackend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookQuotesBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        return Ok();
    }
}