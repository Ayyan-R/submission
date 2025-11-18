namespace Banking_Aggregator_APP.Bank.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; } // "Savings", "Current", "Loan"
        public string Description { get; set; }
    }

}
