namespace Banking_Aggregator_APP.Bank.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;      // REQUIRED
        public string Code { get; set; } = null!;      // USD, INR, EUR...
        public decimal ExchangeRateToBase { get; set; } // decimal(18,2)
    }


}
