using System.ComponentModel.DataAnnotations;
using bank_api.Models;

public class Account {

    public long AccountId {get; set;}

    public float? Balance {get; set;}

    public DateTime? CreatedAt {get; set;}

    public long ClientId {get; set;}

    public long AccountTypeId {get; set;}

    public Client Client {get; set;} = null!;
    
    public AccountType AccountType {get; set;} = null!;
    
}
