using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankCustomerAPI.Service
{
    internal class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int DurationInMinutes { get; set; } = 60;
    }
    public class JwtService
    {


        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId, string username, List<string> roles, List<string> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
         issuer: _configuration["JwtSettings:Issuer"],
         audience: _configuration["JwtSettings:Audience"],
         claims: claims,
         expires: DateTime.Now.AddMinutes(
             double.Parse(_configuration["JwtSettings:DurationInMinutes"])
         ),
         signingCredentials: creds
     );



            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
