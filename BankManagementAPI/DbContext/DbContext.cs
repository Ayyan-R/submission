
using BankManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankCustomerAPI.Data
{
    public class BankCustomerContext : DbContext
    {
        public BankCustomerContext(DbContextOptions<BankCustomerContext> options)
            : base(options)
        {
        }

        // DbSets - match table names
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Account> Accounts { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default DB Schema
            modelBuilder.HasDefaultSchema("training");

            // Composite Keys
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // Relationships
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Bank)
                .WithMany(bk => bk.Branches)
                .HasForeignKey(b => b.BankId);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithMany()
                .HasForeignKey(b => b.ManagerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.PowerOfAttorneyUser)
                .WithMany()
                .HasForeignKey(a => a.PowerOfAttorneyUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
            modelBuilder.Entity<Permission>().HasData(
    new Permission { PermissionId = 1, PermissionName = "CreateUser" },
    new Permission { PermissionId = 2, PermissionName = "DeleteUser" },
    new Permission { PermissionId = 3, PermissionName = "ReadUser" },
    new Permission { PermissionId = 4, PermissionName = "CreateAccount" },
    new Permission { PermissionId = 5, PermissionName = "DeleteAccount" },
    new Permission { PermissionId = 6, PermissionName = "ReadAccount" }
);
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { RoleId = 1, PermissionId = 1 },
                new RolePermission { RoleId = 1, PermissionId = 2 },
                new RolePermission { RoleId = 1, PermissionId = 3 },
                new RolePermission { RoleId = 1, PermissionId = 4 },
                new RolePermission { RoleId = 1, PermissionId = 5 },
                new RolePermission { RoleId = 1, PermissionId = 6 },


                new RolePermission { RoleId = 2, PermissionId = 3 },
                new RolePermission { RoleId = 2, PermissionId = 4 },
                new RolePermission { RoleId = 2, PermissionId = 6 },

                new RolePermission { RoleId = 3, PermissionId = 1 },
                new RolePermission { RoleId = 3, PermissionId = 2 },
                new RolePermission { RoleId = 3, PermissionId = 3 },
                new RolePermission { RoleId = 3, PermissionId = 4 },
                new RolePermission { RoleId = 3, PermissionId = 5 },
                new RolePermission { RoleId = 3, PermissionId = 6 }

            );
            modelBuilder.Entity<User>().HasData(
    new User
    {
        UserId = 1,
        FirstName = "System",
        LastName = "Admin",
        Email = "admin@test.com",
        PasswordHash = PasswordSeeder.AdminPassword,
        DateOfBirth = new DateTime(1990, 1, 1),
        UserType = "Bank"
    },
    new User
    {
        UserId = 2,
        FirstName = "Normal",
        LastName = "User",
        Email = "user@test.com",
        PasswordHash = PasswordSeeder.UserPassword,
        DateOfBirth = new DateTime(1995, 1, 1),
        UserType = "Normal"
    },
    new User
    {
        UserId = 3,
        FirstName = "Admin",
        LastName = "User",
        Email = "adminuser@test.com",
        PasswordHash = PasswordSeeder.AdminUserPassword,
        DateOfBirth = new DateTime(1992, 5, 15),
        UserType = "Bank"
    },
    new User
    {
        UserId = 4,
        FirstName = "Empty",
        LastName = "Role",
        Email = "nouser@test.com",
        PasswordHash = PasswordSeeder.NoUserPassword,
        DateOfBirth = new DateTime(2000, 10, 10),
        UserType = "Normal"
    }
);

       
        }
    }
}