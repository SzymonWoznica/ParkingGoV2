using DomainLayer.Entity;
using InfrastructureLayer.Data;
using InfrastructureLayer.Implementation.Tokens.Creators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _appDbContext;
        public testController(AppDbContext appDbContext,IConfiguration config)
        {
            _config = config;
            _appDbContext = appDbContext;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            User user = new User
            {
                EmailAddress = "a@.a",
                Id = Guid.NewGuid().ToString(),
                Password = "a",
                RoleUser = 0
            };

            JwtGenerator jwt = new JwtGenerator(_config);
            var t = jwt.GenerateToken(user);

            //var token = GenerateToken();
            return Ok(t);
        }

        [HttpGet]
        //[Route("Admins")]
        [Authorize]
        public IActionResult AdminEndPoint()
        {
            return Ok($"Hi you are an");
        }

        // To generate token
        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"username"),
                new Claim(ClaimTypes.Role,"role")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
