using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using team_origin.Entities;
using team_origin.Entities.Notifications;

namespace team_origin
{
    public class TeamOriginContext: IdentityDbContext<User>
    {
        public DbSet<UserNotificationRef> UserNotificationRef { get; set; }
        public DbSet<VerificationCode> VerificationCode { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<FriendshipStatus> FriendshipStatus { get; set; }

        public DbSet<Mood> Mood { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
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

            //modelBuilder for Friendship status table
            modelBuilder
                .Entity<FriendshipStatus>(entity =>
                {
                    entity.HasKey(fs => fs.StatusId);

                    entity.Property(fs => fs.StatusId)
                          .ValueGeneratedOnAdd();
                    entity.Property(fs=>fs.StatusDescription)
                          .HasMaxLength(50);
                });

            //modelBuilder for Friendship table
            modelBuilder
                .Entity<Friendship>(entity =>
                {
                    entity.HasKey(f => f.FrienshipId);

                    entity.Property(f => f.FrienshipId)
                          .ValueGeneratedOnAdd();

                    entity.HasOne(f => f.FromUser)
                        .WithMany(u => u.FromUserFriendship)
                        .HasForeignKey(f => f.FromUserId);

                    entity.HasOne(f => f.ToUser)
                        .WithMany(u => u.ToUserFriendship)
                        .HasForeignKey(f => f.ToUserId);

                    entity.HasOne(f => f.FriendshipStatus)
                    .WithMany(fs => fs.Friendship)
                    .HasForeignKey(f => f.FriendshipStatusId)
                    .IsRequired();
                });

            //For the User's Mood
            modelBuilder
                .Entity<Mood>(entity =>
                {
                    entity.HasKey(m => m.MoodId);

                    entity.Property(m => m.MoodId)
                          .ValueGeneratedOnAdd();

                    entity.HasOne(m => m.User)
                          .WithOne(u => u.Mood)
                          .HasForeignKey<Mood>(u => u.UserId);

                    entity.Property(m => m.MoodDescription)
                        .HasMaxLength(140);
                });
            #region Notification
            //For NotificationType Table
            modelBuilder
                .Entity<NotificationType>(entity =>
                {
                    entity.HasKey(nt => nt.NotificationTypeId);
                    entity.Property(nt => nt.NotificationTypeId)
                    .ValueGeneratedOnAdd();
                    entity.Property(nt => nt.NotificationTypeDescription)
                    .HasMaxLength(200);
                });

            //For NotificationTable
            modelBuilder
                .Entity<Notification>(entity => 
                {
                    entity.HasKey(n => n.NotificationId);
                    entity.Property(n => n.NotificationId)
                    .ValueGeneratedOnAdd();
                    entity.HasOne(n => n.NotificationType)
                    .WithMany(nt => nt.Notification)
                    .HasForeignKey(n => n.NotificationTypeId);
                    entity.HasOne(u => u.User)
                    .WithMany(n => n.Notification)
                    .HasForeignKey(n => n.CreatedBy);
                });
            //For UserNotificationRef Table
            modelBuilder
                .Entity<UserNotificationRef>(entity =>
                {
                    entity.HasKey(nr => nr.UserNotificationRefId);
                    entity.Property(nr => nr.UserNotificationRefId)
                    .ValueGeneratedOnAdd();
                    entity.HasOne(u => u.User)
                    .WithMany(nr => nr.UserNotificationRef)
                    .HasForeignKey(nr => nr.RecipientUserId);
                    entity.HasOne(n => n.Notification)
                    .WithOne(nr => nr.UserNotificationRef)
                    .HasForeignKey<UserNotificationRef>(nr => nr.NotificationId);
                });
#endregion Notification
        }
    }
}
