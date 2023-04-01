using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using bank_api.Models;
using bank_api.Data;
using bank_api.Models.Dtos;

namespace bank_api.Services;

public class AuthService {

    private readonly IConfiguration _config;
    private readonly BankContext _bankContext;
    private readonly ILogger<BankContext> _logger;

    public AuthService(BankContext bankContext, ILogger<BankContext> logger, IConfiguration config){
        _bankContext = bankContext;
        _logger = logger;
        _config = config;
    }

    public async Task<User?> GetUser(LoginDto loginDto){

        try
        {
            var user = await  _bankContext.Users.SingleOrDefaultAsync(  x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            return user;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());

            return null;
        }
    }

    public string GenerateJwt(User user){
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _config.GetSection("Jwt:Key").Value!));

        var credentials = new SigningCredentials( key, SecurityAlgorithms.HmacSha512Signature);


        var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials
            );
        
        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}