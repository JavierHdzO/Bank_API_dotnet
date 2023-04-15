using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using bank_api.Interfaces;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

public class AccountService : IContextService<AccountDto, CreateAccountDto, UpdateAccountDto>
{
    private readonly ILogger _logger;
    private readonly BankContext _bankContext;
    private readonly IMapper _mapper;
    private readonly IValidator<Account> _validator;

    public AccountService(
        ILogger<AccountService> logger,
        BankContext bankContext,
        IMapper mapper,
        IValidator<Account> validator
    ){
        _logger = logger;
        _bankContext = bankContext;
        _mapper = mapper;
        _validator = validator;

    }


    public async Task<ActionResult<AccountDto>> CreateOne(CreateAccountDto createAccountDto)
    {
        try
        {
            var account = new Account();

            _mapper.Map<CreateAccountDto, Account>(createAccountDto, account);

            _bankContext.Accounts.Add(account);

            await _bankContext.SaveChangesAsync();

            return  _mapper.Map<Account, AccountDto>(account);

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

    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
    {
        try {

            var accounts = await _bankContext.Accounts.Select( account => _mapper.Map<Account, AccountDto>( account ) ).ToListAsync();

            return accounts;

        }
        catch(Exception exception){
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult<AccountDto>?> GetOne(long Id)
    {
        try
        {
            var account = await  _bankContext.Accounts.FindAsync(Id);

            if( account is null ) return new NotFoundObjectResult( new { Message = $"Account with {Id} Id was not found" });

            return _mapper.Map<Account, AccountDto>(account);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult> UpdateOne(long Id, UpdateAccountDto updateAccountDto)
    {
        try
        {
            var account = await _bankContext.Accounts.FindAsync(Id);

            if(account is null) return new NotFoundResult();

            _mapper.Map<UpdateAccountDto, Account>(updateAccountDto, account);

            await _bankContext.SaveChangesAsync();

            return new OkObjectResult( new {Message = "Account has been successfully updated"});
        }
        catch (Exception exception)
        {
            
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult> PatchUpdateOne(long Id, JsonPatchDocument<UpdateAccountDto> updateJsonDoc){

        
        try
        {   
            JsonPatchDocument<Account> jsonDoc = new JsonPatchDocument<Account>();
            _mapper.Map<JsonPatchDocument<UpdateAccountDto>, JsonPatchDocument<Account>>(updateJsonDoc, jsonDoc);

            var account = await _bankContext.Accounts.FindAsync(Id);
            if(account is null) return new NotFoundObjectResult( new { Message = "Account was not found"});

            ValidationResult validationResult = await _validator.ValidateAsync(account);

            if( !validationResult.IsValid ) {
                
                return new BadRequestObjectResult(  new {
                    Message = "Ensure the fields have requirements of contraints", 
                    Errors = validationResult.Errors});
            }

            jsonDoc.ApplyTo(account);
            await _bankContext.SaveChangesAsync();

            return new OkObjectResult(new { Message = "Account has been successfully updated"});


        }
        catch (Exception exception)
        {
            
            _logger.LogError(exception, "Server error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}