
using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class LoginDto {

    [Required]
    public string Email {get; set;} = null!;

    [Required]
    [MinLength(8)]
    public string Password {get; set;} = null!;
}