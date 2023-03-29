using Microsoft.AspNetCore.Mvc;
using bank_api.Models;
using bank_api.Interfaces;

namespace bank_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase {

    private readonly IContextService<User> _userService;
    public UserController(
        IContextService<User> userService
    )
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(){

        return await _userService.GetAll();
    }

}