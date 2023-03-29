
namespace bank_api.Models;

public class User {

    public long UserId {get; set;}

    public string Email  { get; set;} = null!;

    public string Password { get; set; } = null!;

    public bool Status {get; set;}

    public DateTime CreatedAt {get; set;}

    public long RoleId {get; set;}

    public Role Role {get; set;} = null!;

    public Client Client {get; set;} = null!;
}