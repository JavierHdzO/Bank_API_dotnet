using Microsoft.AspNetCore.Mvc;
using bank_api.Interfaces;
using bank_api.Models.Dtos;
using bank_api.Services;

namespace bank_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountTypeController: ControllerBase {

    private readonly AccountTypeService _accountTypeService;
    public AccountTypeController(
        IContextService<AccountTypeDto, CreateAccountTypeDto, UpdateAccountTypeDto> accountTypeService
    ){
        _accountTypeService = (AccountTypeService) accountTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountTypeDto>>> GetAllAccountType(){
        return await _accountTypeService.GetAll();
    }

    [HttpPost]
    public async Task<ActionResult<AccountTypeDto>> CreateAccountType([FromBody] CreateAccountTypeDto createAccountTypeDto){
        return await _accountTypeService.CreateOne(createAccountTypeDto);

    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<AccountTypeDto>> GetAccountType([FromRoute] long Id){
        return (await _accountTypeService.GetOne(Id))!;
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdateAccountType([FromRoute] long Id, [FromBody] UpdateAccountTypeDto updateAccountTypeDto){

        return await _accountTypeService.UpdateOne(Id, updateAccountTypeDto);
    }

    

}