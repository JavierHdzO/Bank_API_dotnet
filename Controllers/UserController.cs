using Microsoft.AspNetCore.Mvc;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;

namespace bank_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase {

    private readonly IContextService<UserDto> _userService;
    public UserController(
        IContextService<UserDto> userService
    )
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDto>>> FindUsers(){

        return await _userService.GetAll();
        
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDto>> FindOne([FromRoute] long Id){

        var user = await _userService.GetOne(Id);

        if( user == null ) return  NotFound();

        return user;
    }

}