using BankCustomerAPI.DTO;
using BankManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet("Customer")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult>GetUserData()
        {
            var data = await _userService.GetAll();
            return Ok(data);
        }


        [HttpGet("Employee")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminData()
        {
            return Ok("This is protected admin data.");
        }


        [HttpGet("All")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllData()
        {
            return Ok("This is protected data for both admin and user.");
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request);
            if (updatedUser == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(updatedUser);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound($"User with ID {id} not found.");

            return Ok($"User with ID {id} deleted successfully.");
        }
    }
}
