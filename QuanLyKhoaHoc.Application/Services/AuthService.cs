namespace QuanLyKhoaHoc.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public AuthService(
            IApplicationDbContext context,
            IUser user,
            IEmailService emailService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService)
        {
            _context = context;
            _user = user;
            _emailService = emailService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public async Task<Result> SendConfirmEmail(string email, CancellationToken cancellation)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);

            if (user == null) return Result.Failure();

            var token = _tokenService.GenerateEmailConfirmationToken();

            await _context.ConfirmEmails.AddAsync(new ConfirmEmail()
            {
                UserId = user.Id,
                ConfirmCode = token,
                IsConfirm = false,
                ExpiryTime = DateTime.Now.AddMinutes(30)
            });

            await _context.SaveChangesAsync(cancellation);

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var confirmationLink = $"{baseUrl}/api/auth/confirmEmail?token={token}";

            await _emailService.SendEmailAsync(email, "Mã Xác Nhận", $"Mã Xác Nhận Của Bạn Là: {confirmationLink}");

            return Result.Success();
        }

        public async Task<Result> ConfirmEmail(string token, CancellationToken cancellation)
        {
            var confirmEmail = await _context.ConfirmEmails.Include(c => c.User).FirstOrDefaultAsync(c => c.ConfirmCode == token, cancellation);

            if (confirmEmail == null || confirmEmail.IsConfirm)
            {
                return Result.Failure("Token Không Hợp Lệ Hoặc Tài Khoản Đã Được Kích Hoạt");
            }

            confirmEmail.User.IsActive = true;

            _context.ConfirmEmails.Remove(confirmEmail);

            await _context.SaveChangesAsync(cancellation);

            return Result.Success();
        }

        public async Task<Result> RequestPasswordReset(string email, CancellationToken cancellation)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellation);

            if (user == null || user.Id.ToString() != _user.Id)
            {
                return Result.Failure("Email Không Tồn Tại Hoặc Bạn Không Có Quyền Yêu Cầu Thay Đổi Mật Khẩu");
            }

            var token = _tokenService.GenerateEmailConfirmationToken();

            await SendConfirmEmail(email, cancellation);

            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";
            var confirmLink = $"{domain}/reset-password?token={token}";

            await _emailService.SendEmailAsync(user.Email, "Đặt lại mật khẩu", confirmLink);

            return Result.Success();
        }

        public async Task<TokenRequest?> RefreshAccessToken(string token, CancellationToken cancellation)
        {
            var refreshToken = await _context.RefreshTokens.Include(c => c.User).ThenInclude(c => c.Permissions).ThenInclude(c => c.Role).AsNoTracking().FirstOrDefaultAsync(c => c.Token == token, cancellation);

            if (refreshToken == null || refreshToken.ExpiryTime < DateTime.UtcNow)
                return null;

            var roles = refreshToken.User.Permissions.Select(p => p.Role.RoleName).ToArray() ?? Array.Empty<string>();

            return new TokenRequest
            {
                AccessToken = _tokenService.GenerateAccessToken(refreshToken.UserId.ToString(), roles),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<TokenRequest?> Login(LoginRequest request, CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Permissions).ThenInclude(p => p.Role).AsNoTracking().FirstOrDefaultAsync(c => c.Email == request.Email, cancellation);

            if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _context.RefreshTokens.AddAsync(new RefreshToken() { UserId = user.Id, Token = refreshToken, ExpiryTime = DateTime.Now.AddDays(30) });

                await _context.SaveChangesAsync(cancellation);

                var roles = user.Permissions.Select(p => p.Role.RoleName).ToArray() ?? Array.Empty<string>();

                return new TokenRequest
                {
                    AccessToken = _tokenService.GenerateAccessToken(user.Id.ToString(), roles),
                    RefreshToken = refreshToken
                };
            }

            return null;
        }

        public async Task<Result> Register(RegisterRequest request, CancellationToken cancellation)
        {
            if (_context.Users.Any(c => c.Email == request.Email))
            {
                return Result.Failure("Tên Người Dùng Hoặc Email Đã Tồn Tại");
            }

            var userName = request.Email.Split('@')[0];

            var user = new User
            {
                Email = request.Email,
                Username = userName,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = userName,
                IsActive = false,
            };

            await _context.Users.AddAsync(user, cancellation);

            await _context.SaveChangesAsync(cancellation);

            await SendConfirmEmail(user.Email, cancellation);

            return Result.Success();
        }

        public async Task Logout(string token, CancellationToken cancellation)
        {
            var refreshToken = await _context.RefreshTokens.Include(c => c.User).AsNoTracking().FirstOrDefaultAsync(c => c.Token == token, cancellation);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
            }

            await _context.SaveChangesAsync(cancellation);
        }
    }
}
