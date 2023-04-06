
namespace bank_api.Models;

public class Client {
    
    public long ClientId {get; set;}

    public string Name {get; set;} = null!;

    public string LastName {get; set;} = null!;

    public short  Age {get; set;}

    public string Genre {get; set;} = null!;

    public bool Status {get; set;}

    public DateTime CreatedAt {get; set;}

    public long UserId {get; set;}

    public User User {get; set;} = null!;
}