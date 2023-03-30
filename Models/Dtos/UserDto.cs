using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bank_api.Models.Dtos;

public class UserDto {

    public long? Id {get; set;}

    [Required]
    [EmailAddress]
    [MinLength(5)]
    public string Email  { get; set;} = null!;

    [Required]
    [MinLength(8)]
    [JsonIgnore]
    public string Password { get; set; } = null!;

    public long? RoleId {get; set;}
}