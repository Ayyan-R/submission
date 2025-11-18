using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Banking_Aggregator_APP.Bank.Data;
using Banking_Aggregator_APP.Bank.DTOs.Auth;
using Banking_Aggregator_APP.Bank.Service;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService auth, ITokenService tokenService)
    {
        _auth = auth;
        _tokenService = tokenService;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles([FromServices] AppDbContext db)
    {
        var roles = await db.Roles.ToListAsync();
        return Ok(roles);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _auth.RegisterAsync(dto);
        return Ok("Registered Successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto, [FromServices] AppDbContext db)
    {
        var res = await _auth.LoginAsync(dto);
        return Ok(res);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromServices] AppDbContext db)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var tokens = await db.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();
        db.RefreshTokens.RemoveRange(tokens);
        await db.SaveChangesAsync();
        return Ok("Logged out successfully");
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto, [FromServices] AppDbContext db)
    {
        var tokenEntity = await db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == dto.RefreshToken);
        if (tokenEntity == null || tokenEntity.Expires < DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token");

        var user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == tokenEntity.UserId);
        if (user == null) return Unauthorized();

        // Create new access token
        var newToken = _tokenService.CreateToken(user);

        // Optionally generate a new refresh token
        var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        tokenEntity.Token = newRefreshToken;
        tokenEntity.Expires = DateTime.UtcNow.AddDays(7);

        db.RefreshTokens.Update(tokenEntity);
        await db.SaveChangesAsync();

        var userDto = new UserDto(user.Id, user.FullName, user.Email);

        return Ok(new TokenResponseDto(
            Token: newToken,
            ExpiresAt: DateTime.UtcNow.AddHours(6),
            User: userDto,
            RefreshToken: newRefreshToken
        ));
    }
}
