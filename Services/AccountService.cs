using Microsoft.EntityFrameworkCore;
using AutoMapper;
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

    public AccountService(
        ILogger<AccountService> logger,
        BankContext bankContext,
        IMapper mapper
    ){
        _logger = logger;
        _bankContext = bankContext;
        _mapper = mapper;

    }


    public async Task<ActionResult<AccountDto>> CreateOne(CreateAccountDto createAccountDto)
    {
        try
        {
            var account = new Account();

            _mapper.Map<CreateAccountDto, Account>(createAccountDto, account);

            _bankContext.Accounts.Add(account);

            await _bankContext.SaveChangesAsync();

            return new CreatedAtActionResult( 
                nameof(CreateOne), 
                "CreateAccount",
                new { Id = account.AccountId}, 
                _mapper.Map<Account, AccountDto>(account));

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

    public Task<ActionResult> UpdateOne(long Id, UpdateAccountDto updateAccountDto)
    {
        throw new NotImplementedException();
    }
}