namespace Banking_Aggregator_APP.Bank.Models
{
    public enum TicketStatus
    {
        Open = 1,
        InProgress = 2,
        Closed = 3
    }

    public class SupportTicket
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }

        public TicketStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
