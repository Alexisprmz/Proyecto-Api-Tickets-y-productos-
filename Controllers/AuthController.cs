using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    public AuthController(JwtService jwtService) { _jwtService = jwtService; }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (dto.Username == "admin" && dto.Password == "Admin123")
        {
            var token = _jwtService.GenerateToken(dto.Username, "admin");
            return Ok(new { token });
        }
        return Unauthorized();
    }
}
