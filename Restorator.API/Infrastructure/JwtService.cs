using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Restorator.Domain.Services;

namespace Restorator.API.Infrastructure
{
    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _key;
        public JwtService(IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"];

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        }

        public string CreateToken(int userId, string role)
        {
            var claims = new List<Claim>
            {
                new("id", userId.ToString()),
                new(ClaimTypes.Role, role)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(7),
                TokenType = JwtBearerDefaults.AuthenticationScheme,
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
