namespace bank_api.Models;

public class Role {
    
    public long RoleId {get; set;}

    public string Type {get; set;} = null!;

    public DateTime CreatedAt {get; set;}
    
    public List<User> Users {get; set;} = null!;

}