using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Banks> Banks => Set<Banks>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<AccountType> AccountTypes => Set<AccountType>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<LoginAttempt> LoginAttempts => Set<LoginAttempt>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.FullName).IsRequired().HasMaxLength(250);
                b.Property(u => u.Email).IsRequired().HasMaxLength(200);

                b.HasOne(u => u.Role)
                 .WithMany()
                 .HasForeignKey(u => u.RoleId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // ROLE
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Name).IsRequired().HasMaxLength(100);
            });

            // BANK
            modelBuilder.Entity<Banks>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.BankName).IsRequired().HasMaxLength(200);
            });

            // BRANCH
            modelBuilder.Entity<Branch>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Bank)
                 .WithMany(x => x.Branches)
                 .HasForeignKey(x => x.BankId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ACCOUNT
            modelBuilder.Entity<Account>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.AccountNumber).IsRequired().HasMaxLength(50);
                b.Property(x => x.Balance).HasColumnType("decimal(18,2)");

                b.HasOne(x => x.User)
                 .WithMany(x => x.Accounts)
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.Branch)
                 .WithMany(x => x.Accounts)
                 .HasForeignKey(x => x.BranchId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.AccountType)
                 .WithMany()
                 .HasForeignKey(x => x.AccountTypeId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Currency)
                 .WithMany()
                 .HasForeignKey(x => x.CurrencyId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // TRANSACTION
            modelBuilder.Entity<Transaction>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Amount)
                    .HasPrecision(18, 2);

                b.Property(x => x.AmountInBaseCurrency)
                    .HasPrecision(18, 2); // IMPORTANT FIX

                b.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                b.HasOne(x => x.Account)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // CURRENCY
            modelBuilder.Entity<Currency>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Code).IsRequired().HasMaxLength(10);
                b.Property(x => x.ExchangeRateToBase).HasColumnType("decimal(18,4)");
            });

            // ACCOUNT TYPE
            modelBuilder.Entity<AccountType>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
 
    
        public static class SeedData
        {
            public static void EnsureSeedData(AppDbContext db)
            {
                db.Database.Migrate();

                // -------------------------
                // 1. Roles
                // -------------------------
                if (!db.Roles.Any())
                {
                    db.Roles.AddRange(new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                });

                    db.SaveChanges();
                }

                var adminRoleId = db.Roles.First(r => r.Name == "Admin").Id;
                var userRoleId = db.Roles.First(r => r.Name == "User").Id;

                // -------------------------
                // 2. Admin User
                // -------------------------
                if (!db.Users.Any(u => u.Email == "admin@bank.com"))
                {
                    db.Users.Add(new User
                    {
                        FullName = "System Administrator",
                        Email = "admin@bank.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        RoleId = adminRoleId
                    });

                    db.SaveChanges();
                }

                // -------------------------
                // 3. Sample Normal User
                // -------------------------
                if (!db.Users.Any(u => u.Email == "user@bank.com"))
                {
                    db.Users.Add(new User
                    {
                        FullName = "Demo User",
                        Email = "user@bank.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                        RoleId = userRoleId
                    });

                    db.SaveChanges();
                }

            // -------------------------
            // 4. Currencies
            // -------------------------
            if (!db.Currencies.Any())
            {
                db.Currencies.AddRange(
                    new Currency { Name = "US Dollar", Code = "USD", ExchangeRateToBase = 1.0m },
                    new Currency { Name = "Indian Rupee", Code = "INR", ExchangeRateToBase = 83.0m },
                    new Currency { Name = "Euro", Code = "EUR", ExchangeRateToBase = 0.92m }
                );
            }


            // -------------------------
            // 5. Account Types
            if (!db.Currencies.Any())
            {
                db.Currencies.AddRange(
                    new Currency { Name = "US Dollar", Code = "USD", ExchangeRateToBase = 1.0m },
                    new Currency { Name = "Indian Rupee", Code = "INR", ExchangeRateToBase = 83.0m },
                    new Currency { Name = "Euro", Code = "EUR", ExchangeRateToBase = 0.92m }
                );
            }

            // -------------------------
            // 6. Banks
            // -------------------------
            if (!db.Banks.Any())
                {
                    var bank = new Banks
                    {
                        BankName = "State Bank of India"
                    };

                    db.Banks.Add(bank);
                    db.SaveChanges();

                    // -------------------------
                    // 7. Branches
                    // -------------------------
                    db.Branches.Add(new Branch
                    {
                        BranchName = "Chennai Branch",
                        BankId = bank.Id
                    });

                    db.SaveChanges();
                }

                // -------------------------
                // NO ACCOUNTS SEEDED (User creates)
                // -------------------------
            }
        }
    }


