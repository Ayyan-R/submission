using Microsoft.EntityFrameworkCore;
using BankManagementAPI.Models;

namespace BankCustomerAPI.Data
{
    public class BankCustomerContext : DbContext
    {
        public BankCustomerContext(DbContextOptions<BankCustomerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Account> Accounts { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Use your schema
            modelBuilder.HasDefaultSchema("training");

            // --- UserRole (Many-to-Many: User ↔ Role) ---
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // --- RolePermission (Many-to-Many: Role ↔ Permission) ---
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // --- Bank ↔ Branch (One-to-Many) ---
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Bank)
                .WithMany(bk => bk.Branches)
                .HasForeignKey(b => b.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Branch ↔ Manager (User) ---
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithMany()
                .HasForeignKey(b => b.ManagerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- User ↔ Account (One-to-Many) ---
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Account ↔ Branch (One-to-Many) ---
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Power of Attorney User (Self-Reference) ---
            modelBuilder.Entity<Account>()
                .HasOne(a => a.PowerOfAttorneyUser)
                .WithMany()
                .HasForeignKey(a => a.PowerOfAttorneyUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Account>()
    .Property(a => a.Balance)
    .HasPrecision(18, 2); // total digits=18, decimals=2


            // --- Account ↔ Transaction (One-to-Many) ---

            // --- Configure decimal precision (fixes warning) ---
            modelBuilder.Entity<Account>()
                .Property(a => a.InterestRate)
                .HasPrecision(10, 4);

            // --- Common field constraints ---
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .IsRequired();

            modelBuilder.Entity<Permission>()
                .Property(p => p.PermissionName)
                .IsRequired();

            modelBuilder.Entity<Bank>()
                .Property(b => b.BankName)
                .IsRequired();

            modelBuilder.Entity<Branch>()
                .Property(b => b.BranchCode)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
