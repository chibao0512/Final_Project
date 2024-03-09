using Final_Project.DTOs;
using Final_Project.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Responses;
using static Final_Project.Responses.CustomResponses;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountrepo;
        public AccountController(IAccount accountrepo)
        {
            this.accountrepo = accountrepo;
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponses>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountrepo.RegistrationAsync(model);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponses>> LoginAsync(LoginDTO model)
        {
            var result = await accountrepo.LoginAsync(model);
            return Ok(result);
        }
    }
}
