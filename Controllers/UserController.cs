using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;

namespace bank_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase {

    private readonly IContextService<UserDto, CreateUserDto, UpdateUserDto> _userService;

    private readonly IPasswordHasher<CreateUserDto> _passwordHasher;
    public UserController(
        IContextService<UserDto, CreateUserDto, UpdateUserDto> userService,
        IPasswordHasher<CreateUserDto> passwordHasher
    )
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDto>>> FindUsers(){

        return await _userService.GetAll();
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto createUserDto){
        createUserDto.Password =  _passwordHasher.HashPassword(createUserDto, createUserDto.Password);
        return await _userService.CreateOne(createUserDto);

    }

    
    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> FindOne([FromRoute] long Id){

        var user = await _userService.GetOne(Id);

        if( user == null ) return  NotFound();

        return user;
    }


    [Authorize( Policy = "Manager" )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] long Id){
        
        var resp = await _userService.DeleteOne(Id);
        return resp ? Ok( new { Message = "User has been deleted successfully"}) : BadRequest(); 
    }

}