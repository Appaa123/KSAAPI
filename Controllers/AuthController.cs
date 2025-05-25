using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace KSAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]

        public IActionResult Login([FromBody] User user)
        {
            var isUserValidated = _authService.validateUser(user);

            Console.WriteLine("uservalidated : "+isUserValidated);
            Console.WriteLine("user : "+user);

            if (isUserValidated)
            {
                var token = this.generateJWTToken(user.username);
                return Ok(new { token });
            }

            return Unauthorized();

        }

        [HttpPost("verify-token")]
            public IActionResult VerifyToken([FromBody] string token)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");

                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Issuer"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(10) // optional: remove tolerance
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    return Ok(new
                    {
                        valid = true,
                        claims = jwtToken.Claims.Select(c => new { c.Type, c.Value })
                    });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { valid = false, error = ex.Message });
                }
            }

        private string generateJWTToken(string username)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
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