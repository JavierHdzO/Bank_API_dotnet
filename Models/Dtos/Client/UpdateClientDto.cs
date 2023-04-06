using System.ComponentModel.DataAnnotations;

namespace bank_api.Models.Dtos;

public class UpdateClientDto {
    
    [MaxLength(50)]
    [MinLength(2)]
    public string? Name {get; set;} = null!;

    [MaxLength(50)]
    [MinLength(2)]
    public string? LastName {get; set;} = null!;

    [Range(18, 150)]
    public short?  Age {get; set;}

    [StringLength(1)]
    public string? Genre {get; set;} = null!;


    
}