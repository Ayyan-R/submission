namespace BankManagementAPI.Request
{
   
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public List<string>? Roles { get; set; }
        
    }

        public class RegisterRequest
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? UserType { get; set; }
        



    }
}
