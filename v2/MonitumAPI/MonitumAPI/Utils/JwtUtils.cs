using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MonitumAPI.Utils
{
    public class JwtUtils
    {
        private readonly IConfiguration _configuration;
        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJWTToken(string role) 
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120); // valid for 2 hours
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim("role", role));


            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: permClaims,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
    }
}
