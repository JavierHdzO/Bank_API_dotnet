using Microsoft.EntityFrameworkCore;
using bank_api.Data;
using bank_api.Models;
using bank_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bank_api.Services;

public class UserService : IContextService<User>
{
    public Task<ActionResult<User>> CreateOne(User obj)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeleteOne(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<User>> GetOne()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateOne(long Id, User obj)
    {
        throw new NotImplementedException();
    }
}