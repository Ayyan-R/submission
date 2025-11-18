using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Banking_Aggregator_APP.Bank.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Banking_Aggregator_APP.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Create new account
        [HttpPost]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> Create(AccountCreateDto dto)
        {
            // Optional: allow only users to create for themselves
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("SysAdmin") && userIdClaim != dto.UserId.ToString())
                return Forbid();

            var account = await _accountService.CreateAccountAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
        }

        // Get specific account
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null) return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Forbid();

            var userId = int.Parse(userIdClaim);
            var isAdmin = User.IsInRole("SysAdmin");

            if (!isAdmin && account.UserId != userId)
                return Forbid();

            return Ok(account);
        }

        // List all accounts for user
        [HttpGet("my")]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> GetMyAccounts()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Forbid();

            var userId = int.Parse(userIdClaim);
            var accounts = await _accountService.GetAccountsByUserIdAsync(userId);
            return Ok(accounts);
        }

        // List all accounts (admin only)
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        // Close account
        [HttpPost("{id}/close")]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> Close(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null) return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdClaim);
            var isAdmin = User.IsInRole("SysAdmin");

            if (!isAdmin && account.UserId != userId)
                return Forbid();

            await _accountService.CloseAccountAsync(id);
            return NoContent();
        }
        [HttpGet("myaccounts")]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> GetMyAccounts([FromServices] AppDbContext db)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var isAdmin = User.IsInRole("SysAdmin");

            var accounts = isAdmin
                ? await db.Accounts.Include(a => a.User).Include(a => a.Branch).ToListAsync()
                : await db.Accounts.Include(a => a.Branch).Where(a => a.UserId == userId).ToListAsync();

            return Ok(accounts);
        }

    }
}
