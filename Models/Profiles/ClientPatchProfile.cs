using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using bank_api.Models;
using bank_api.Models.Dtos;

namespace bank_api.Models.Profiles;


public class ClientPatchProfile: Profile 
{
    public ClientPatchProfile(){
        CreateMap<JsonPatchDocument<UpdateClientDto>, JsonPatchDocument<Client>>(MemberList.Source);
        CreateMap<Operation<UpdateClientDto>, Operation<Client>>();
    }
}