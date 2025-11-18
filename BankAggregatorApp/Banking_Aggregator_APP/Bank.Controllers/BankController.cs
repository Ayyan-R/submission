using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Banking_Aggregator_APP.Bank.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Aggregator_APP.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly IBankRepository _bankRepo;
        public BanksController(IBankRepository bankRepo) { _bankRepo = bankRepo; }


        [HttpPost]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Create(BankCreateDto dto)
        {
            var bank = new Banks{ BankName = dto.BankName };
            await _bankRepo.AddAsync(bank);
            await _bankRepo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = bank.Id }, bank);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var bank = await _bankRepo.GetWithBranchesAsync(id);
            if (bank == null) return NotFound();
            return Ok(bank);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await _bankRepo.GetByIdAsync(id);
            if (bank == null) return NotFound();

            _bankRepo.Remove(bank);
            await _bankRepo.SaveChangesAsync();
            return Ok("Bank deleted successfully");
        }

    }
}
