using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using team_origin.Entities;

namespace team_origin
{
    public class TeamOriginContext: IdentityDbContext<User>
    {
        public DbSet<VerificationCode> VerificationCode { get; set; }

        public TeamOriginContext(DbContextOptions<TeamOriginContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Call OnModelCreating method of IdentityDbContext<User>
            base.OnModelCreating(modelBuilder);

            // Override/Update configurations from Identity 
            // User Table
            modelBuilder
                .Entity<User>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable(nameof(User));

            modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .HasMaxLength(50);

            // Role Table
            modelBuilder
                .Entity<IdentityRole>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("Role");

            modelBuilder
                .Entity<IdentityRole>()
                .Property(role => role.Id)
                // over-write default names that is prefixed with "AspNet"
                .HasMaxLength(50);

            // UserClaim Table
            modelBuilder
                .Entity<IdentityUserClaim<string>>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("UserClaim");

            // UserRole Table
            modelBuilder
                .Entity<IdentityUserRole<string>>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("UserRole");

            modelBuilder
                .Entity<IdentityUserRole<string>>()
                .Property(userRole => userRole.RoleId)
                .HasMaxLength(50);

            // UserLogin Table
            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("UserLogin");

            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                .Property(userlogin => userlogin.LoginProvider)
                .HasMaxLength(200);

            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                .Property(userlogin => userlogin.ProviderKey)
                .HasMaxLength(200);

            // RoleClaim Table
            modelBuilder
                .Entity<IdentityRoleClaim<string>>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("RoleClaim");

            // UserToken Table
            modelBuilder
                .Entity<IdentityUserToken<string>>()
                // over-write default names that is prefixed with "AspNet"
                .ToTable("UserToken");

            modelBuilder
                .Entity<IdentityUserToken<string>>()
                .Property(token => token.UserId)
                .HasMaxLength(50);

            modelBuilder
                .Entity<IdentityUserToken<string>>()
                .Property(token => token.Name)
                .HasMaxLength(200);

            modelBuilder
                .Entity<IdentityUserToken<string>>()
                .Property(token => token.LoginProvider)
                .HasMaxLength(200);

            modelBuilder
                .Entity<VerificationCode>(entity =>
            {
                entity.HasKey(vc => vc.Id);

                entity.Property(vc => vc.Id)
                      .ValueGeneratedOnAdd();

                entity.HasOne(vc => vc.User)
                      .WithOne(u => u.VerificationCode)
                      .HasForeignKey<VerificationCode>(u => u.UserId);
            });
        }
    }
}
