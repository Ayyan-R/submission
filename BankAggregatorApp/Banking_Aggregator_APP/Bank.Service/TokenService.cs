namespace Banking_Aggregator_APP.Bank.Service
{
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
   
    using global::Banking_Aggregator_APP.Bank.Models;

    public interface ITokenService
    {
        string CreateToken(User user);
    }


    public class TokenService : ITokenService
    {
        private readonly IConfiguration _cfg;
        private readonly SymmetricSecurityKey _key;


        public TokenService(IConfiguration cfg)
        {
            _cfg = cfg;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]));
        }


        public string CreateToken(User user)
        {
            var claims = new List<Claim>
{
new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
};


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
            issuer: _cfg["Jwt:Issuer"],
            audience: _cfg["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(60),
            signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
