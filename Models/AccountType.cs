using AutoMapper.Configuration.Annotations;
using bank_api.Models;

public class AccountType {

    public long AccountTypeId {get; set;}

    public string Name {get; set;} = null!;

    public DateTime? RegDate {get; set;}

    public List<Account> Accounts {get; set;} = null!;
}