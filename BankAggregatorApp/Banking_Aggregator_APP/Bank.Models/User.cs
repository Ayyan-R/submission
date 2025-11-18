using System.Data;
using System.Security.Principal;

namespace Banking_Aggregator_APP.Bank.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }

}
