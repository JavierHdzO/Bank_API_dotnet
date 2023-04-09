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

        CreateMap<UpdateClientDto, Client>()
            .AddTransform<string>( val => val.ToUpper())
            .ForMember( dest => dest.Genre, options => options.MapFrom(
                    (source, dest) => 
                        (source.Genre != null && (source.Genre.Equals("M") || source.Genre.Equals("F")))
                        ? source.Genre 
                        : dest.Genre ))
            .ForMember(dest => dest.Age, options => options.MapFrom(
                    (source, dest) => 
                        (source.Age  >= 18 && source.Age <= 150) 
                        ? source.Age
                        : dest.Age ))
            .ForMember(dest => dest.ClientId, options => options.MapFrom( (source, dest ) => dest.ClientId ))
            .ForMember(dest => dest.Status, options => options.MapFrom(( source, dest) => dest.Status))
            .ForMember(dest =>  dest.UserId, options => options.MapFrom( (source, dest ) => dest.ClientId ))
            .ForMember(dest => dest.User, options => options.MapFrom((source, dest) => dest.User))
            .ForMember(dest => dest.CreatedAt, options => options.MapFrom((source, dest) => dest.CreatedAt));

        CreateMap<Client, ClientDto>();
    }
}