using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Services;

namespace AgileTaskMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO)
        {
            var authResponse = await _authService.Login(loginDTO);
            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }
    }
}
