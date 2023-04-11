using Microsoft.EntityFrameworkCore;
using bank_api.Models;

namespace bank_api.Data;

public class BankContext: DbContext {

    public DbSet<User> Users {get; set;} = null!;
    public DbSet<Role> Roles {get; set;} = null!;

    public DbSet<Client> Clients {get; set;} = null!;

    public BankContext(DbContextOptions<BankContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        //Roles Entity
        modelBuilder.Entity<Role>(
            roleEntity => {
                roleEntity.Property( role => role.Type ).IsRequired();
                roleEntity.Property( role => role.CreatedAt ).HasDefaultValueSql("now()");

            }
        );

        //User entity
        modelBuilder.Entity<User>( 
            userEntity => {
                userEntity.Property( user => user.Email ).IsRequired();
                userEntity.Property( user => user.Password ).IsRequired();
                userEntity.Property( user => user.Status ).HasDefaultValue(true);
                userEntity.Property( user => user.RoleId ).HasDefaultValue(1);
                userEntity.Property( user => user.CreatedAt).HasDefaultValueSql("now()");

                userEntity
                    .HasOne( user => user.Role)
                    .WithMany( role =>  role.Users );

                userEntity.HasAlternateKey( user => user.Email );
                
            }
            
        );

        //Client Entity
        modelBuilder.Entity<Client>(
            clientEntity => {
                clientEntity.Property( client => client.Name )
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                clientEntity.Property( client => client.LastName ).HasColumnType("varchar(50)");
                clientEntity.Property( client => client.Genre ).HasColumnType("varchar(1)");
                clientEntity.Property( client => client.Age )
                    .IsRequired()
                    .HasColumnType("smallint");
                clientEntity.Property( client => client.Status ).HasDefaultValue(true);
                clientEntity.Property( client => client.UserId ).IsRequired();
                clientEntity.Property( client => client.CreatedAt ).HasDefaultValueSql("now()");

                clientEntity
                    .HasOne( client => client.User )
                    .WithOne( user => user.Client );
                
                clientEntity.HasAlternateKey( client => client.UserId);

            }
        );

        // Account Entity
        modelBuilder.Entity<Account>( 
            accountEntity => {
                accountEntity.Property( account => account.Balance )
                    .IsRequired()
                    .HasColumnType("money")
                    .HasDefaultValue(0);
                accountEntity.Property( account => account.CreatedAt ).HasDefaultValueSql("now()");
                accountEntity.Property( account => account.ClientId ).IsRequired();

                accountEntity
                    .HasOne( account  => account.Client )
                    .WithMany( client => client.Accounts );

                accountEntity
                    .HasOne( account => account.AccountType )
                    .WithMany( accountType => accountType.Accounts );

            }
        );

        // Account Entity 

        modelBuilder.Entity<AccountType>( 
            accountTypeEntity =>{
                accountTypeEntity.Property( accountType => accountType.Name ).IsRequired().HasColumnType("varchar(100)");
                accountTypeEntity.Property( accountType => accountType.RegDate ).HasDefaultValueSql("now()");

            }
        );
        
        
    }
}