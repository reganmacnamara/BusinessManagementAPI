using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MacsBusinessManagementAPI.Infrastructure.Authentication.Service
{

    public class AuthService(IOptions<JwtConfig> jwtSettings) : IAuthService
    {
        private readonly JwtConfig _JwtSettings = jwtSettings.Value;

        public string GenerateToken(long accountId, string email)
        {
            var _Key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_JwtSettings.Secret));

            var _Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var _Token = new JwtSecurityToken(
                issuer: _JwtSettings.Issuer,
                audience: _JwtSettings.Audience,
                claims: _Claims,
                expires: DateTime.UtcNow.AddMinutes(_JwtSettings.ExpiryMinutes),
                signingCredentials: new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hash)
            => BCrypt.Net.BCrypt.Verify(password, hash);
    }

}
