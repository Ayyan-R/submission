using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BankManagementAPI.Request;
using BankManagementAPI.Services;
using System.Threading.Tasks;
using BankCustomerAPI.DTO;
using BankManagementAPI.Models;

namespace BankManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }
      

        [HttpPost("Register")]
         
        public async Task<IActionResult> Register([FromBody] RegisterRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Email and Password are required.");

            // Determine if this is the first user
            bool isFirstUser = !await _userService.AnyUsersExistAsync();

            var role = isFirstUser
                ? "Admin"
                : (string.IsNullOrWhiteSpace(user.UserType) ? "User" : user.UserType);

            var result = await _userService.RegisterAsync(
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password,
                role
            );

            if (!result)
                return BadRequest("A user with this email already exists.");

            return Ok($"User registered successfully as {role}.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email and Password are required.");

            var token = await _userService.LoginAsync(request.Email, request.Password);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }

        
        
    }
}
