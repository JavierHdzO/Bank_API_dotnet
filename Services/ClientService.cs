using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using bank_api.Interfaces;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;

namespace bank_api.Services;

public class ClientService : IContextService<ClientDto, CreateClientDto, UpdateClientDto>
{
    private readonly ILogger _logger;
    private readonly BankContext _bankContext;
    private readonly IMapper _mapper;

    public ClientService(
        ILogger<ClientService> logger,
        BankContext bankContext,
        IMapper mapper
    ){
        _logger = logger;
        _bankContext = bankContext;
        _mapper = mapper;

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
            
            return new CreatedResult(nameof(CreateOne), _mapper.Map<Client, ClientDto>(client));
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
            var clients = await _bankContext.Clients.Select( client => _mapper.Map<Client, ClientDto>(client) ).ToListAsync();

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

            return _mapper.Map<Client, ClientDto>(client);
        }
        catch (Exception exception)
        {   
            _logger.LogError(exception, "If the problem persist to report to admin");

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ActionResult> UpdateOne(long Id, UpdateClientDto updateClientDto)
    {
        try
        {
            var client = await _bankContext.Clients.SingleAsync( client => client.ClientId == Id && client.Status == true);

            if(client is null) return new NotFoundObjectResult( new { Message = $"Client with Id = {Id} not found"});

            _mapper.Map<UpdateClientDto, Client>(updateClientDto, client);

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();

            await _bankContext.SaveChangesAsync();

            return new OkObjectResult( new { Message = "Client has been updated"});

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Server Error ");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }


    public async Task<ActionResult<ClientDto>> PatchUpdateOne(long Id, JsonPatchDocument<UpdateClientDto> patchDoc)
    {
        JsonPatchDocument<Client> patchDocClient = new JsonPatchDocument<Client>();

        try
        {
            patchDocClient = _mapper.Map(patchDoc,patchDocClient);

            if( patchDoc == null || patchDocClient == null) return new BadRequestResult();

            var client = await _bankContext.Clients.FindAsync(Id);

            if(client is null || client.Status == false ) return new NotFoundResult();

            patchDocClient.ApplyTo(client);

            _mapper.Map<Client, Client>(client, client);

            _logger.LogInformation(client.Name);

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();

            await _bankContext.SaveChangesAsync();

            return _mapper.Map<Client, ClientDto>(client);
            
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}