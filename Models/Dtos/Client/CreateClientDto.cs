
namespace bank_api.Models.Dtos;

public class CreateClientDto {
    public long ClientId {get; set;}

    public string Name {get; set;} = null!;

    public string LastName {get; set;} = null!;

    public short  Age {get; set;}

    public string Genre {get; set;} = null!;

    public long UserId {get; set;}
}