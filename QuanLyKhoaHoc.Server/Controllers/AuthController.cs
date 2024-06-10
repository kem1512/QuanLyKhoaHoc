namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _jwtService;

        public AuthController(IAuthService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenRequest>> Login(LoginRequest request, CancellationToken cancellation)
        {
            return Ok(await _jwtService.Login(request, cancellation));
        }

        [HttpPost("logout")]
        public async Task Logout(string token, CancellationToken cancellation)
        {
            await _jwtService.Logout(token, cancellation);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellation)
        {
            return Ok(await _jwtService.Register(request, cancellation));
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenRequest>> Refresh(string refreshToken, CancellationToken cancellation)
        {
            return Ok(await _jwtService.RefreshAccessToken(refreshToken, cancellation));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, CancellationToken cancellation)
        {
            return Ok(await _jwtService.ConfirmEmail(token, cancellation));
        }

        [HttpPost("SendConfirmEmail")]
        public async Task<ActionResult> SendConfirmEmail(CancellationToken cancellation)
        {
            return Ok(await _jwtService.SendConfirmEmail(cancellation));
        }

        [HttpGet("info")]
        public async Task<ActionResult<TokenRequest>> Info(CancellationToken cancellation)
        {
            return Ok(await _jwtService.UserInfo(cancellation));
        }
    }
}
