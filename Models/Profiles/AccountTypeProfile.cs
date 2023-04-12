using AutoMapper;
using bank_api.Models;
using bank_api.Models.Dtos;


namespace bank_api.Models.Profiles;

public class AccountTypeProfile: Profile {

    public AccountTypeProfile()
    {
        CreateMap<AccountType, AccountTypeDto>();

        CreateMap<CreateAccountTypeDto, AccountType>()
            .AddTransform<String>( val => val.ToUpper() )
            .ForMember( dest => dest.AccountTypeId, options => options.Ignore() )
            .ForMember( dest => dest.RegDate, options => options.Ignore() ) 
        ;


        CreateMap<UpdateAccountTypeDto, AccountType>()
            .AddTransform<String>( val => val.ToUpper() )
            .ForMember( dest => dest.AccountTypeId, options => options.Ignore() )
            .ForMember( dest => dest.RegDate, options => options.Ignore() );
    }

}