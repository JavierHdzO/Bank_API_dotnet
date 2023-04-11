using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class CreateAccountDto {
    
    [Range(0.0f, float.MaxValue)]
    public float? Balance {get; set;}

    [Required]
    [Range(1, long.MaxValue)]
    public long ClientId {get; set;}

    [Required]
    [Range(1, long.MaxValue)]
    public long AccountTypeId {get; set;}

}