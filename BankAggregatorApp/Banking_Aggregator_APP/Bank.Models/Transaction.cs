namespace Banking_Aggregator_APP.Bank.Models
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdrawal = 2,
        Transfer = 3
    }

    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }  // enum

        public DateTime CreatedAt { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public decimal AmountInBaseCurrency { get; set; } // after conversion


        public string Reference { get; set; }  // “Deposit”, “Transfer to X”, etc.
        public int PerformedByUserId { get; set; }
    }

}
