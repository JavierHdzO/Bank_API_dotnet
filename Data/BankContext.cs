using Microsoft.EntityFrameworkCore;
using bank_api.Models;

namespace bank_api.Data;

public class BankContext: DbContext {

    public DbSet<User> Users {get; set;} = null!;
    public DbSet<Role> Roles {get; set;} = null!;

    public BankContext(DbContextOptions<BankContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        modelBuilder.Entity<Role>(
            roleEntity => {
                roleEntity.Property( role => role.Type ).IsRequired();
                roleEntity.Property( role => role.CreatedAt ).HasDefaultValueSql("now()");

            }
        );

        modelBuilder.Entity<User>( 
            userEntity => {
                userEntity.Property( user => user.Email ).IsRequired();
                userEntity.Property( user => user.Password ).IsRequired();
                userEntity.Property( user => user.Status ).HasDefaultValue(true);
                // userEntity.Property( user => user.Role ).HasDefaultValue(1);
                userEntity.Property( user => user.CreatedAt).HasDefaultValueSql("now()");

                userEntity
                    .HasOne( user => user.Role)
                    .WithMany( role =>  role.Users );
                
            }
        );

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
                clientEntity.Property( client => client.CreatedAt ).HasDefaultValueSql("now()");

                clientEntity
                    .HasOne( client => client.User )
                    .WithOne( user => user.Client );
                

            }
        );
        
        
    }
}