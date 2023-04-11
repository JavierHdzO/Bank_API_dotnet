
namespace bank_api.Models.Dtos;

public class AccountDto {
    
    public long AccountId {get; set;}

    public float? Balance {get; set;}

    public DateTime? CreatedAt {get; set;}

    public long ClientId {get; set;}

    public long AccountTypeId {get; set;}
}