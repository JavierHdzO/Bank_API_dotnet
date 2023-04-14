
using FluentValidation;
using bank_api.Models;

namespace bank_api.Models.Validators;

public class AccountValidator : AbstractValidator<Account> {

    public AccountValidator(){
        RuleFor( account => account.Balance )
            .NotNull()
            .GreaterThanOrEqualTo(0).ExclusiveBetween(0, float.MaxValue);
        
        RuleFor( account => account.AccountId )
            .NotNull();
        
        RuleFor( account => account.AccountTypeId )
            .NotNull();

        RuleFor( account => account.ClientId )
            .NotNull();
    }
}