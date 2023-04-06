using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class UpdateClientDto {
    
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string Name {get; set;} = null!;

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string LastName {get; set;} = null!;

    [Required]
    [Range(18, 150)]
    public short  Age {get; set;}

    [Required]
    [StringLength(1)]
    public string Genre {get; set;} = null!;
}