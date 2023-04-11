using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class UpdateAccountDto {

    [Required]
    [Range(0.0f, float.MaxValue)]
    public float Balance {get; set;}
    
}