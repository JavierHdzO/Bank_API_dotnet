using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bank_api.Models;
using bank_api.Data;
using bank_api.Models.Dtos;

namespace bank_api.Services;

public class AuthService {

    private readonly BankContext _bankContext;
    private readonly ILogger<BankContext> _logger;

    public AuthService(BankContext bankContext, ILogger<BankContext> logger){
        _bankContext = bankContext;
        _logger = logger;
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
}