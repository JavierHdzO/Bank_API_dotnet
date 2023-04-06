using Microsoft.EntityFrameworkCore;
using bank_api.Interfaces;
using bank_api.Data;
using bank_api.Models;
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

    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        if( _bankContext.Clients == null ) return  ParallelEnumerable.Empty<ClientDto>();

        try
        {
            var clients = await _bankContext.Clients.Select( client => ToClientDto( client ) ).ToListAsync();

            return clients;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        }

    }
 
    public Task<ActionResult<ClientDto>?> GetOne(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOne(long Id, UpdateClientDto obj)
    {
        throw new NotImplementedException();
    }


    private static ClientDto ToClientDto(Client client){

        return new ClientDto{
            Name = client.Name,
            LastName =  client.LastName,
            Age = client.Age,
            Genre = client.Genre
        };

    }
}