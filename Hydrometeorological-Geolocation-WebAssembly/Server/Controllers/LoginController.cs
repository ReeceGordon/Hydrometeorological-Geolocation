using Hydrometeorological_Geolocation_Prototype_V2.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// Login controller handles management of login system as documented in other files. Token expiry is set to 2 minutes
/// meaning that auser will be logged out after 2 minutes and re-login will be required
/// </summary>
namespace Hydrometeorological_Geolocation_Prototype_V2.Server.Controllers
{

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration) => _configuration = configuration;

        [HttpPost("api/login")]

        public LoginResult Login(Credentials credentials)
        {
            var expiry = DateTime.Now.AddMinutes(2);
            return ValidateCredentials(credentials) ? new LoginResult { Token = GenerateJWT(credentials.Email, expiry), Expiry = expiry } : new LoginResult();
        }

        bool ValidateCredentials(Credentials credentials)
        {
            var user = _configuration.GetSection("Credentials").Get<Credentials>();
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.VerifyHashedPassword(null, user.Password, credentials.Password) == PasswordVerificationResult.Success;
        }

        private string GenerateJWT(string email, DateTime expiry)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                new[] { new Claim(ClaimTypes.Name, email) },
                expires: expiry,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}