using AutoMapper.Configuration.Annotations;

namespace bank_api.Models;

public class Client {
    
    [Ignore]
    public long ClientId {get; set;}

    public string Name {get; set;} = null!;

    public string LastName {get; set;} = null!;

    public short  Age {get; set;}

    public string Genre {get; set;} = null!;

    [Ignore]
    public bool? Status {get; set;}

    public DateTime CreatedAt {get; set;}

    [Ignore]
    public long UserId {get; set;}

    [Ignore]
    public User User {get; set;} = null!;
}