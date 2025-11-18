using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Service
{
    public interface IBankRepository : IGenericRepository<Banks>
    {
        Task<Banks> GetWithBranchesAsync(int id);
    }


    public class BankRepository : GenericRepository<Banks>, IBankRepository
    {
        public BankRepository(AppDbContext db) : base(db) { }
        public async Task<Banks> GetWithBranchesAsync(int id) => await _db.Banks.Include(b => b.Branches).FirstOrDefaultAsync(b => b.Id == id);
    }
}
