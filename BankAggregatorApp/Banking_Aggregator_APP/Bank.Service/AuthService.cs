using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Banking_Aggregator_APP.Bank.Service
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task RegisterAsync(RegisterDto dto);
    }


    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;


        public AuthService(IUserRepository userRepo, AppDbContext db, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _db = db;
            _tokenService = tokenService;
        }


        public async Task RegisterAsync(RegisterDto dto)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists) throw new InvalidOperationException("Email already in use");


            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId
            };


            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }


        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null) throw new UnauthorizedAccessException("Invalid credentials");

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid) throw new UnauthorizedAccessException("Invalid credentials");

            // Create access token
            var token = _tokenService.CreateToken(user);

            // --- Added refresh token code ---
            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            await _db.RefreshTokens.AddAsync(refreshTokenEntity);
            await _db.SaveChangesAsync();
            // --- End added code ---

            // Create response with user info, include refresh token
            var response = new TokenResponseDto(
                token,
                DateTime.UtcNow.AddHours(6),
                new UserDto(user.Id, user.FullName, user.Email),
                refreshToken // <-- added refresh token to response
            );

            return response;
        }

        // --- Helper method for refresh token ---
        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }


    }
}
