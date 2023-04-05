using Microsoft.EntityFrameworkCore;
using bank_api.Interfaces;
using bank_api.Data;
using bank_api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace bank_api.Services;

public class ClientService : IContextService<ClientDto, CreateClientDto, UpdateClientDto>
{

    private readonly ILogger _logger;
    private readonly BankContext _bankContext;

    public ClientService(
        ILogger<ClientService> logger,
        BankContext bankContext
    ){
        _logger = logger;
        _bankContext = bankContext;

    }

    public Task<ActionResult<ClientDto>> CreateOne(CreateClientDto obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOne(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ClientDto>?> GetOne(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOne(long Id, UpdateClientDto obj)
    {
        throw new NotImplementedException();
    }
}