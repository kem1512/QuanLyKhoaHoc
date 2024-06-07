using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenRequest>> Login(LoginRequest request, CancellationToken cancellation)
        {
            return Ok(await _jwtService.Login(request, cancellation));
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

        [HttpGet("info")]
        public async Task<ActionResult<TokenRequest>> Info(CancellationToken cancellation)
        {
            return Ok(await _jwtService.UserInfo(cancellation));
        }
    }
}
