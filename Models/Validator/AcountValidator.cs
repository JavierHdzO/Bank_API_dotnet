
using FluentValidation;
using bank_api.Models;

namespace bank_api.Models.Validators;

public class AccountValidator : AbstractValidator<Account> {

    public AccountValidator(){
        RuleFor( account => account.Balance )
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(float.MaxValue);
    }
}