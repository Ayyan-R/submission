using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Service
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(AccountCreateDto dto);
        Task<Account> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int userId);
        Task<IEnumerable<Account>> GetAllAccountsAsync(); // for admin
        Task CloseAccountAsync(int accountId);
    }

    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;

        public AccountService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Account> CreateAccountAsync(AccountCreateDto dto)
        {
            var user = await _db.Users.FindAsync(dto.UserId) ?? throw new InvalidOperationException("User not found");
            var branch = await _db.Branches.FindAsync(dto.BranchId) ?? throw new InvalidOperationException("Branch not found");

            var account = new Account
            {
                AccountNumber = GenerateAccountNumber(),
                UserId = dto.UserId,
                BranchId = dto.BranchId,
                AccountTypeId = dto.AccountTypeId,
                CurrencyId = dto.CurrencyId,
                Balance = 0m,
                IsClosed = false
            };

            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _db.Accounts
                .Include(a => a.User)
                .Include(a => a.Branch)
                .Include(a => a.AccountType)
                .Include(a => a.Currency)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int userId)
        {
            return await _db.Accounts
                .Where(a => a.UserId == userId)
                .Include(a => a.Branch)
                .Include(a => a.AccountType)
                .Include(a => a.Currency)
                .ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _db.Accounts
                .Include(a => a.User)
                .Include(a => a.Branch)
                .Include(a => a.AccountType)
                .Include(a => a.Currency)
                .ToListAsync();
        }

        public async Task CloseAccountAsync(int accountId)
        {
            var account = await _db.Accounts.FindAsync(accountId) ?? throw new InvalidOperationException("Account not found");
            account.IsClosed = true;
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }

        private string GenerateAccountNumber()
        {
            // Unique account number generator: timestamp + random
            return $"{DateTime.UtcNow.Ticks}{new Random().Next(1000, 9999)}";
        }
    }
}
