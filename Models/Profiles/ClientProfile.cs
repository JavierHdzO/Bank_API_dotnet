using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using bank_api.Models;
using bank_api.Models.Dtos;

namespace bank_api.Models.Profiles;


public class ClientProfile: Profile 
{
    public ClientProfile(){
        CreateMap<JsonPatchDocument<UpdateClientDto>, JsonPatchDocument<Client>>();
        CreateMap<Operation<UpdateClientDto>, Operation<Client>>();
        
        CreateMap<Client, Client>().AddTransform<string>(val => val.ToUpper());

        CreateMap<UpdateClientDto, Client>().AddTransform<string>( val => val.ToUpper());

        CreateMap<Client, ClientDto>();
    }
}