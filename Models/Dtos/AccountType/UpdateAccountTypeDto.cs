using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class UpdateAccountTypeDto {

    [MinLength(3)]
    [MaxLength(60)]
    [Required]
    public string Name {get; set;} = null!;
}