using System.Security.Principal;

namespace Banking_Aggregator_APP.Bank.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        public int BankId { get; set; }
        public Banks Bank { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }

}
