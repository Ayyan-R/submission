using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banking_Aggregator_APP.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BranchesController(AppDbContext db) { _db = db; }


        [HttpPost]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> Create(BranchCreateDto dto)
        {
            var branch = new Branch { BankId = dto.BankId, BranchName = dto.BranchName };
            await _db.Branches.AddAsync(branch);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = branch.Id }, branch);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var branch = await _db.Branches.Include(b => b.Bank).FirstOrDefaultAsync(b => b.Id == id);
            if (branch == null) return NotFound();
            return Ok(branch);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SysAdmin")]
        public async Task<IActionResult> DeleteBranch(int id, [FromServices] AppDbContext db)
        {
            var branch = await db.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            db.Branches.Remove(branch);
            await db.SaveChangesAsync();
            return Ok("Branch deleted successfully");
        }

    }
}
