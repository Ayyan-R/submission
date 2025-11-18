using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Banking_Aggregator_APP.Bank.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_Aggregator_APP.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Create transaction (withdraw/deposit)
        [HttpPost]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> Create(TransactionCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var isAdmin = User.IsInRole("SysAdmin");

            var tx = await _transactionService.CreateTransactionAsync(dto, userId, isAdmin);
            return Ok(tx);
        }

        // Get transactions by account
        [HttpGet("account/{accountId}")]
        [Authorize(Roles = "User,SysAdmin")]
        public async Task<IActionResult> GetByAccount(int accountId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var isAdmin = User.IsInRole("SysAdmin");

            var transactions = await _transactionService.GetTransactionsByAccountAsync(accountId, userId, isAdmin);
            return Ok(transactions);
        }

        // Get all transactions (admin only)
        [HttpGet]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }
    }
}
