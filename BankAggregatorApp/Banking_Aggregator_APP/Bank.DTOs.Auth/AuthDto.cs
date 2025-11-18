using Banking_Aggregator_APP.Bank.Models;

namespace Banking_Aggregator_APP.Bank.DTOs.Auth

{
    public record RegisterDto(string FullName, string Email, string Password, int RoleId);
    public record LoginDto(string Email, string Password);
    public record UserDto(int Id, string FullName, string Email);




    public record TokenResponseDto(string Token, DateTime ExpiresAt, UserDto User, string RefreshToken);
    public record BankCreateDto(string BankName);
    public record BranchCreateDto(int BankId, string BranchName);
    public record AccountCreateDto(int UserId, int BranchId, int AccountTypeId, int CurrencyId);
    public record TransactionCreateDto(int AccountId, decimal Amount, TransactionType Type, int CurrencyId, string Reference);
    public record RefreshTokenRequestDto(string RefreshToken);
}
 
