using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace KSAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("login")]

        public  IActionResult Login([FromBody] User user)
        {
            if(user.Username == "admin" && user.Password == "password"){

                var token =  this.generateJWTToken(user.Username);
                return Ok(new {token});
            }

            return Unauthorized();

        }


        private string generateJWTToken(string username){

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]??""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials                
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}