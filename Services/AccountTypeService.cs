using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using bank_api.Interfaces;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;

namespace bank_api.Services;

public class AccountTypeService : IContextService<AccountTypeDto, CreateAccountTypeDto, UpdateAccountTypeDto>
{
    private readonly ILogger _logger;
    private readonly BankContext _bankContext;

    private readonly IMapper _mapper;

    public AccountTypeService (
        ILogger<AccountTypeService> logger,
        BankContext bankContext,
        IMapper mapper

    ){
        _logger = logger;
        _bankContext = bankContext;
        _mapper = mapper;
    }

    public async Task<ActionResult<AccountTypeDto>> CreateOne(CreateAccountTypeDto createAccountTypeDto)
    {
        try
        {
            var accountType =  new AccountType();
            _mapper.Map<CreateAccountTypeDto, AccountType>(createAccountTypeDto, accountType);

            _bankContext.AccountTypes.Add(accountType);

            await _bankContext.SaveChangesAsync();

            return _mapper.Map<AccountType, AccountTypeDto>(accountType);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public Task<bool> DeleteOne(long Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<IEnumerable<AccountTypeDto>>> GetAll()
    {
        try{

            var accountType = await _bankContext.AccountTypes.Select( accountType => _mapper.Map<AccountType, AccountTypeDto>(accountType)).ToListAsync();

            return accountType;

        }catch(Exception exception)
        {
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult<AccountTypeDto>?> GetOne(long Id)
    {
        try{
            var accountType  = await _bankContext.AccountTypes.FindAsync(Id);

            if(accountType is null) return new  NotFoundObjectResult(new { Message = $"Account Type with {Id} Id not found"});

            return _mapper.Map<AccountType, AccountTypeDto>(accountType);
        }
        catch(Exception exception){

            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult> UpdateOne(long Id, UpdateAccountTypeDto updateAccountTypeDto)
    {
        try{

            var accountType = await  _bankContext.AccountTypes.SingleAsync( accountType => accountType.AccountTypeId == Id);

            _mapper.Map<UpdateAccountTypeDto, AccountType>(updateAccountTypeDto, accountType);

            await _bankContext.SaveChangesAsync();

            return new OkObjectResult( new { Message = "Account Type has been successfully updated"} );

        }
        catch(Exception exception){
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult( StatusCodes.Status500InternalServerError);
        }
    }


    public async Task<ActionResult> PatchUpdateOne(long Id, JsonPatchDocument<UpdateAccountTypeDto> jsonPatchDocumentUpdate){

        try
        {
            JsonPatchDocument<AccountType> jsonPatchDocument = new JsonPatchDocument<AccountType>();
            _mapper.Map<JsonPatchDocument<UpdateAccountTypeDto>, JsonPatchDocument<AccountType>>(jsonPatchDocumentUpdate, jsonPatchDocument);

            var accountType = await _bankContext.AccountTypes.SingleAsync( accountType => accountType.AccountTypeId == Id);

            jsonPatchDocument.ApplyTo(accountType);

            await _bankContext.SaveChangesAsync();

            return new OkObjectResult(new { Message = "Account Type has been successfully updated"});


        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}