using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProteinStore.API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProteinStore.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminAuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AdminAuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto dto)
        {
            var adminEmail = _config["Admin:Email"];
            var adminPassword = _config["Admin:Password"];

            if (dto.Email != adminEmail || dto.Password != adminPassword)
                return Unauthorized("Invalid admin credentials");

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, dto.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
     issuer: _config["Jwt:Issuer"],
     audience: _config["Jwt:Audience"],
     claims: claims,
     expires: DateTime.UtcNow.AddHours(6),
     signingCredentials: new SigningCredentials(
         key, SecurityAlgorithms.HmacSha256)
 );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}