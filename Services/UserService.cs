using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace bank_api.Services;

public class UserService : IContextService<UserDto, CreateUserDto, UpdateUserDto>
{
    private readonly  BankContext _bankContext;
    private readonly ILogger _logger;

    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
            BankContext bankContext,   
            ILogger<UserService> logger,
            IPasswordHasher<User> passwordHasher
         )
    {
        _bankContext = bankContext;
        _logger = logger;
        _passwordHasher = passwordHasher;

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

    public async Task<ActionResult<UserDto>> CreateOne(CreateUserDto createUserDto)
    {
        var user = new User{
            Email = createUserDto.Email,
            Password = createUserDto.Password
        };

        _passwordHasher.HashPassword(user, user.Password);

        try
        {
            _bankContext.Users.Add(user);
            await _bankContext.SaveChangesAsync();


            return new  CreatedResult(nameof(CreateOne), ToUserDto(user) );
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            return new StatusCodeResult(500);
        }
    }

    public async Task<bool> DeleteOne(long Id)
    {
        if(_bankContext.Users == null ) return  false;

        try
        {
            var user = await _bankContext.Users.FindAsync(Id);
            
            if( user == null ) return false;

            user.Status = false;

            await _bankContext.SaveChangesAsync();

            return true;

        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());
            throw;
        }
        
    }



    public Task<ActionResult> UpdateOne(long Id, UpdateUserDto obj)
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

    public Task<ActionResult<UserDto>> PatchUpdateOne(long Id, JsonPatchDocument jsonPatchDocument)
    {
        throw new NotImplementedException();
    }
}