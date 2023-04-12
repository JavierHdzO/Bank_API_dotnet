using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using bank_api.Services;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;

namespace bank_api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientController: ControllerBase {

    private readonly ClientService _clientService;

    public ClientController(
        IContextService<ClientDto, CreateClientDto, UpdateClientDto> clientService
    ){
        _clientService =  (ClientService) clientService;
    }
    
    [Authorize(Policy = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients(){

        return await _clientService.GetAll();
    }

    [Authorize(Policy = "Manager")]
    [HttpPost]
    public async Task<ActionResult<ClientDto>> PostClient([FromBody] CreateClientDto createClientDto){

        return await _clientService.CreateOne(createClientDto);
    }


    [HttpGet("{Id}")]
    public async Task<ActionResult<ClientDto>> GetClient([FromRoute] long Id){
        var client = await _clientService.GetOne(Id);

        return client!;
    }

    [Authorize(Policy = "Manager")]
    [HttpPatch("{Id}")]
    public async Task<ActionResult<ClientDto>> PatchUpdateOne(long Id, [FromBody] JsonPatchDocument<UpdateClientDto> patchDoc){
        return await _clientService.PatchUpdateOne(Id, patchDoc);
    }

    [Authorize(Policy = "Manager")]
    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdateClient(long Id, [FromBody] UpdateClientDto updateClientDto){
        return await _clientService.UpdateOne(Id, updateClientDto);
    }

    [Authorize(Policy = "Manager")]
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteClient([FromRoute] long Id){

        var result =  await _clientService.DeleteOne(Id);

        return (result) 
            ? Ok(new {Message = "Client has beed deleteed"}) 
            : BadRequest(new { Message = "Client has not been deleted"});
    }

}
