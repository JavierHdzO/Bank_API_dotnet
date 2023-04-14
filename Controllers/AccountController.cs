using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using bank_api.Services;
using bank_api.Interfaces;
using bank_api.Models;
using bank_api.Models.Dtos;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase 
{

    private readonly AccountService _accountService;

    public AccountController(
        IContextService<AccountDto, CreateAccountDto, UpdateAccountDto> accountService
    ){
        _accountService = ( AccountService ) accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts(){
        return await _accountService.GetAll();
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<AccountDto>> GetAccount([FromRoute] long Id){
        return (await _accountService.GetOne(Id))!;
    }

    [HttpPost]
    public async Task<ActionResult<AccountDto>> CreateAccount([FromBody] CreateAccountDto createAccountDto){
        return await _accountService.CreateOne(createAccountDto);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAccount([FromRoute] long Id, [FromBody] UpdateAccountDto updateAccountDto){
        return await _accountService.UpdateOne(Id, updateAccountDto);
    }


    [HttpPatch("{Id}")]
    public async Task<IActionResult> PatchUpdateAccount([FromRoute] long Id, [FromBody] JsonPatchDocument<UpdateAccountDto> pathDoc){
        return await _accountService.PatchUpdateOne(Id, pathDoc);
    }
    

    // public Task<IActionResult> DeleteAccounts(){}
}