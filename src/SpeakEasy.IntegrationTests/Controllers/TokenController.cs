using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            var symmetricKey = Convert.FromBase64String("really long secret name that is used to generate jwt token");
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, "username")}),
                Issuer = "test authority",
                Expires = DateTime.UtcNow.AddMinutes(3600),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new
            {
                issuedBy = "tests",
                token,
                meta = "meta"
            });
        }
    }
}
