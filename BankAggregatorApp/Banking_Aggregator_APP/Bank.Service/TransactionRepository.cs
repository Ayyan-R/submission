using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;


namespace Banking_Aggregator_APP.Bank.Service
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(TransactionCreateDto dto, int userId, bool isAdmin);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId, int userId, bool isAdmin);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync(); // Admin only
    }
  

namespace Banking_Aggregator_APP.Bank.Service
    {
        public class TransactionService : ITransactionService
        {
            private readonly AppDbContext _db;

            public TransactionService(AppDbContext db) => _db = db;

            public async Task<Transaction> CreateTransactionAsync(TransactionCreateDto dto, int userId, bool isAdmin)
            {
                var account = await _db.Accounts.Include(a => a.Currency).FirstOrDefaultAsync(a => a.Id == dto.AccountId);
                if (account == null) throw new InvalidOperationException("Account not found");

                // Permission check
                if (!isAdmin && account.UserId != userId)
                    throw new UnauthorizedAccessException("Not allowed to transact on this account");

                // Insufficient funds check
                if (dto.Type == TransactionType.Withdrawal && account.Balance < dto.Amount)
                    throw new InvalidOperationException("Insufficient funds");

                var tx = new Transaction
                {
                    AccountId = dto.AccountId,
                    Amount = dto.Amount,
                    Type = dto.Type,
                    CurrencyId = dto.CurrencyId,
                    AmountInBaseCurrency = dto.Amount * account.Currency.ExchangeRateToBase,
                    Reference = dto.Reference,
                    CreatedAt = DateTime.UtcNow
                };

                if (dto.Type == TransactionType.Deposit) account.Balance += dto.Amount;
                if (dto.Type == TransactionType.Withdrawal) account.Balance -= dto.Amount;

                await _db.Transactions.AddAsync(tx);
                _db.Accounts.Update(account);
                await _db.SaveChangesAsync();

                return tx;
            }

            public async Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId, int userId, bool isAdmin)
            {
                var account = await _db.Accounts.FindAsync(accountId);
                if (account == null) throw new InvalidOperationException("Account not found");

                if (!isAdmin && account.UserId != userId)
                    throw new UnauthorizedAccessException("Not allowed to view this account");

                return await _db.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
            }

            public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
            {
                return await _db.Transactions.Include(t => t.Account).ThenInclude(a => a.User).ToListAsync();
            }
        }
    }

}
