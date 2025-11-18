using System.Transactions;

namespace Banking_Aggregator_APP.Bank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsClosed { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }

}
