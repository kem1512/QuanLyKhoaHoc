namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo(CancellationToken cancellation) {
            return Ok(await _accountService.UserInfo(cancellation));
        }

        [HttpGet("RegisterStudy")]
        public async Task<IActionResult> RegisterStudy(int courseId, CancellationToken cancellation)
        {
            return Ok(await _accountService.RegisterStudy(courseId, cancellation));
        }

        [HttpPost]
        public async Task<IActionResult> UserInfoUpdate(UserInfoUpdate entity, CancellationToken cancellation)
        {
            return Ok(await _accountService.UserInfoUpdate(entity, cancellation));
        }
    }
}
