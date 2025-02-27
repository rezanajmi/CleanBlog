using CleanBlog.Domain.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanBlog.Infrastructure.Identity
{
    internal class JwtService : IJwtService
    {
        private readonly JwtSettings jwtSettings;
        public JwtService(IOptions<JwtSettings> options)
        {
            jwtSettings = options.Value;
        }

        public string GenerateToken(Guid guid, string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, guid.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        // Another code for generate token
        //public string GenerateToken(Guid guid, string username)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));

        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, guid.ToString()),
        //        new Claim(ClaimTypes.Name, username)
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings.Issuer,
        //        audience: jwtSettings.Audience,
        //        claims: claims,
        //        notBefore: null,
        //        expires: DateTime.UtcNow.AddDays(10),
        //        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
