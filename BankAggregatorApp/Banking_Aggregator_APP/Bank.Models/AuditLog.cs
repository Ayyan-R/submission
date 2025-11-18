namespace Banking_Aggregator_APP.Bank.Models
{
     public class AuditLog
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public string Action { get; set; }  // e.g., "Created Account", "Deposit"
        public string TableName { get; set; }
        public string RecordId { get; set; }  // string for flexibility
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }

}
