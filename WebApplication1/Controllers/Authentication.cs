using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Request;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class Authentication : ControllerBase
    {
        private readonly IBranchAccountRepository _repo;
        private readonly IConfiguration _configuration;

        public Authentication(IBranchAccountRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request.Email == null || request.Password == null) return BadRequest();

            var account = await _repo.GetByEmail(request.Email);

            if (account == null || account.AccountPassword != request.Password) return NotFound("Invalid credentials.");

            return Ok("Token: " + GenerateToken(account));
        }

        private string GenerateToken(BranchAccount? account)
        {
            SymmetricSecurityKey SecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials Credentials = new(SecurityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.EmailAddress),
                new Claim(ClaimTypes.Role, account.Role.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
