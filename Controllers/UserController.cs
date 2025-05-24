using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSAApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         private IConfiguration _configuration;
          private IUserService _authInterface;

        public UserController(IConfiguration configuration, IUserService authService)
        {
            _configuration = configuration;
            _authInterface = authService;

        }

        [HttpPost("add-user")]

        public async Task<User> AddUser([FromBody] User user)
        {

            if (user.username != null || user.password != null)
            {
                await this._authInterface.AddUserAsync(user);
            }

            return user;

        }
    }
}