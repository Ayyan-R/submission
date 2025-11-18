namespace Banking_Aggregator_APP.Bank.Models
{
    public class LoginAttempt
    {
       
            public int Id { get; set; }   // <-- Primary Key

            public string Email { get; set; }
        public DateTime AttemptedAt { get; set; }
            public bool IsSuccessful { get; set; }
        }

    }

