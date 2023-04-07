using AutoMapper;
using bank_api.Models;
using bank_api.Models.Dtos;

namespace bank_api.Models.Profiles;


public class ClientProfile: Profile 
{
    public ClientProfile(){
        CreateMap<UpdateClientDto, Client>();
    }
}