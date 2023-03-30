using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;

namespace bank_api.Services;

public class UserService : IContextService<UserDto>
{
    private readonly  BankContext _bankContext;
    private readonly ILogger<BankContext> _logger;

    public UserService(
        BankContext bankContext,
        ILogger<BankContext> logger )
    {
        _bankContext = bankContext;
        _logger = logger;

    }

    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        try
        {
            return await _bankContext.Users.Select( user => ToUserDto(user)).ToListAsync();
        }
        catch (Exception exception)
        {   
            _logger.LogError(exception.ToString());
            
            return new StatusCodeResult(500);
        }
    }

    public async Task<ActionResult<UserDto>?> GetOne(long Id)
    {
        try
        {
            var user = await _bankContext.Users.FindAsync( Id );

            if(user == null) return null;

            return ToUserDto(user);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            
            return new StatusCodeResult(500);
        }
    }

    public async Task<ActionResult<UserDto>> CreateOne(UserDto userDto)
    {
        var user = new User{
            Email = userDto.Email,
            Password = userDto.Password
        };

        try
        {
            _bankContext.Users.Add(user);
            await _bankContext.SaveChangesAsync();

            userDto.RoleId = user.RoleId;

            return new  CreatedAtActionResult(nameof(CreateOne),"Create", null, userDto );
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            return new StatusCodeResult(500);
        }
    }

    public Task<IActionResult> DeleteOne(long Id)
    {
        throw new NotImplementedException();
    }



    public Task<IActionResult> UpdateOne(long Id, UserDto obj)
    {
        throw new NotImplementedException();
    }


    private static UserDto ToUserDto(User user){
        return new UserDto{
            Id = user.UserId,
            Email = user.Email,
            RoleId = user.RoleId
        };
    }
}