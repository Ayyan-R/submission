namespace Banking_Aggregator_APP.Bank.Models
{
     public class Banks
    {
        public int Id { get; set; }
        public string BankName { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }

}
