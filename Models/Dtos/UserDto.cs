
using System.Text.Json.Serialization;

namespace bank_api.Models.Dtos;

public class UserDto {

    public long? Id {get; set;}

    public string Email  { get; set;} = null!;

    [JsonIgnore]
    public string Password { get; set; } = null!;

    public long? RoleId {get; set;}
}