using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using bank_api.Models;
using bank_api.Models.Dtos;

public class AccountProfile : Profile 
{
    public AccountProfile(){

        CreateMap<Account, AccountDto>();

        CreateMap<CreateAccountDto, Account>()
            .ForMember( dest => dest.AccountId, options => options.Ignore())
            .ForMember( dest => dest.CreatedAt, options => options.Ignore() )
            .ForMember( dest => dest.Client, options => options.Ignore() )
            .ForMember( dest => dest.AccountType, options => options.Ignore() );
        
        CreateMap<UpdateAccountDto, Account>()
            .ForMember( dest => dest.AccountId, options => options.Ignore())
            .ForMember( dest => dest.CreatedAt, options => options.Ignore() )
            .ForMember( dest => dest.Client, options => options.Ignore() )
            .ForMember( dest => dest.AccountType, options => options.Ignore() );

        CreateMap<JsonPatchDocument<UpdateAccountDto>, JsonPatchDocument<Account>>();

        CreateMap<Operation<UpdateAccountDto>, Operation<Account>>();
    }
}