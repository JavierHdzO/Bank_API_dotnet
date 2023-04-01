using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using bank_api.Services;
using bank_api.Models.Dtos;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase {

    private readonly AuthService _authService;
    private readonly ILogger<AuthService> _logger;
    public AuthController( AuthService authService, ILogger<AuthService> logger ){
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto){

        var user = await _authService.GetUser(loginDto);

        if(user is null) return BadRequest(new { message = "Invalid Credentials"});

        var token = _authService.GenerateJwt(user);

        if(token ==  null || token.IsNullOrEmpty() ) return Unauthorized(new { message = "Can not generate your personal token"});

        return Ok(new { token = token });
    }

}
