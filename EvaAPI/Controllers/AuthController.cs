using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EvaAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EvaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public ActionResult<string> GetToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey
                (Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claimsForToken =
            [
                new Claim("sub", user.Id.ToString()),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
            ];

            var jwtSecurityToken = new JwtSecurityToken
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);

        }
        
        
    }
}
