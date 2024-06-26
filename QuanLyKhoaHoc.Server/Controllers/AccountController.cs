﻿namespace QuanLyKhoaHoc.Server.Controllers
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
        public async Task<UserInfo?> UserInfo(CancellationToken cancellation) {
            return await _accountService.UserInfo(cancellation);
        }

        [HttpGet("RegisterStudy")]
        public async Task<RegisterStudyMapping?> RegisterStudy(int courseId, CancellationToken cancellation)
        {
            return await _accountService.RegisterStudy(courseId, cancellation);
        }

        [HttpPost]
        public async Task<Result> UserInfoUpdate(UserInfoUpdate entity, CancellationToken cancellation)
        {
            return await _accountService.UserInfoUpdate(entity, cancellation);
        }

        [HttpPost("ChangePassword")]
        public async Task<Result> ChangePassword(string currentPassword, string newPassword, CancellationToken cancellation)
        {
            return await _accountService.ChangePassword(currentPassword, newPassword, cancellation);
        }
    }
}
