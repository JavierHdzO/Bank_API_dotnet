
using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class CreateUserDto {

    [Required]
    [EmailAddress]
    public string Email {get; set;} = null!;

    [Required]
    [MinLength(8)]
    public string Password {get; set;} = null!;

}