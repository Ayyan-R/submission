using Banking_Aggregator_APP.Bank.Data;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Service
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        public GenericRepository(AppDbContext db) => _db = db;


        public async Task AddAsync(T entity) { await _db.Set<T>().AddAsync(entity); }
        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _db.Set<T>().FindAsync(id);
        public void Remove(T entity) { _db.Set<T>().Remove(entity); }
        public void Update(T entity) { _db.Set<T>().Update(entity); }
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
