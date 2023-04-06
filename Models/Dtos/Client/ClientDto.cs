using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class ClientDto {
     public long ClientId {get; set;}

    public string Name {get; set;} = null!;

    public string LastName {get; set;} = null!;

    public short  Age {get; set;}

    public string Genre {get; set;} = null!;
}