using BankCustomerAPI.Data;
using BankCustomerAPI.DTO;
using BankCustomerAPI.Service;
using BankManagementAPI.Controllers;
using BankManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BankManagementAPI.Services
{
    public class UserService
    {
        private readonly BankCustomerContext _context;
        private readonly JwtService _jwtService;

        public UserService(BankCustomerContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
            if (user == null) return null;


            using var sha256 = SHA256.Create();
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));

            if (user.PasswordHash != passwordHash)
                return null;


             var userRoles = new List<string>();
            if (user.UserType == "Admin")
                userRoles.Add("Admin");
            else if (user.UserType == "User")
                userRoles.Add("User");
            else if (user.UserType == "UserAndAdmin")
            {
                userRoles.Add("Admin");
                userRoles.Add("User");
            }
            else
                userRoles.Add("NonUser");


            var token = _jwtService.GenerateToken(
                userId: user.UserId,
                username: $"{user.FirstName} {user.LastName}",
                roles: userRoles,
                permissions: new List<string>()
            );

            return token;
        }


        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password, string role = "User")
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            using var sha256 = SHA256.Create();
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                UserType = role,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IActionResult>  GetAll()
        {
           var Result= await _context.Users.ToListAsync();
            return (IActionResult)Result;
        }

        public async Task<bool> AnyUsersExistAsync()
        {
            return await _context.Users.AnyAsync(u => !u.IsDeleted);
        }




        public async Task<User?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsDeleted)
                return null;

            if (!string.IsNullOrWhiteSpace(request.Name))
                user.FirstName = request.Name; // assuming name maps to FirstName
            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                using var sha256 = SHA256.Create();
                user.PasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
            }
            if (!string.IsNullOrWhiteSpace(request.Role))
                user.UserType = request.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsDeleted)
                return false;

            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}