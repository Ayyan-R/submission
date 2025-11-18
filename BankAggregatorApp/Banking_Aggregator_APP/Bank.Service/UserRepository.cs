using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Service
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Users .Include(u => u.Role) .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
