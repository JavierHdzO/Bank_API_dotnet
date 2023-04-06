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

    public async Task<ActionResult<ClientDto>> CreateOne(CreateClientDto createClientDto)
    {
        var client = new Client{
            Name = createClientDto.Name.ToUpper().Trim(),
            LastName = createClientDto.LastName.ToUpper().Trim(),
            Age =  createClientDto.Age,
            Genre = createClientDto.Genre,
            UserId = createClientDto.UserId
        };

        try
        {
            _bankContext.Clients.Add(client);

            await _bankContext.SaveChangesAsync();
            
            return new CreatedResult(nameof(CreateOne), ToClientDto(client));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "If the problem persist to report to admin");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<bool> DeleteOne(long Id)
    {
        try
        {
            var client = await _bankContext.Clients.FindAsync(Id);

            if(client is null || client.Status == false ) return false;

            client.Status = false;

            await _bankContext.SaveChangesAsync();

            return true;

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "If the problem persist to report to admin");
            return false;
        }
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
 
    public async Task<ActionResult<ClientDto>?> GetOne(long Id)
    {
        try
        {
            var client = await _bankContext.Clients.FindAsync( Id );

            if(client is null) return new NotFoundResult();

            return ToClientDto(client);
        }
        catch (Exception exception)
        {   
            _logger.LogError(exception, "If the problem persist to report to admin");

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public Task<bool> UpdateOne(long Id, UpdateClientDto obj)
    {
        throw new NotImplementedException();
    }


    private static ClientDto ToClientDto(Client client){

        return new ClientDto{
            ClientId = client.ClientId,
            Name = client.Name,
            LastName =  client.LastName,
            Age = client.Age,
            Genre = client.Genre,
            UserId = client.UserId
        };

    }
}