using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyKhoaHoc.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IOptions<JwtSettings> jwtSettings, IApplicationDbContext context, IUser user, IEmailService emailService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
            _user = user;
            _emailService = emailService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> SendConfirmEmail(string token, CancellationToken cancellation)
        {
            if (_user.Id == null) return Result.Failure("Bạn Không Thể Yêu Cầu Gửi Email");

            await _context.ConfirmEmails.AddAsync(new ConfirmEmail() { UserId = int.Parse(_user.Id), ConfirmCode = token, IsConfirm = false, ExpiryTime = DateTime.Now.AddMinutes(30) });

            await _context.SaveChangesAsync(cancellation);

            return Result.Success();
        }

        public async Task<Result> ConfirmEmail(string token, CancellationToken cancellation)
        {
            var confirmEmail = await _context.ConfirmEmails.FirstOrDefaultAsync(c => c.ConfirmCode == token, cancellation);

            if (confirmEmail == null || confirmEmail.IsConfirm)
            {
                return Result.Failure("Token Không Hợp Lệ Hoặc Tài Khoản Đã Được Kích Hoạt");
            }

            confirmEmail.IsConfirm = true;

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

            var token = GenerateEmailConfirmationToken();

            await SendConfirmEmail(token, cancellation);

            var request = _httpContextAccessor.HttpContext.Request;

            var domain = $"{request.Scheme}://{request.Host}";

            var confirmLink = $"{domain}/reset-password?token={token}";

            await _emailService.SendEmailAsync(user.Email, "Đặt lại mật khẩu", confirmLink);

            return Result.Success();
        }

        public async Task<UserMapping> UserInfo(CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Province).Include(c => c.District).Include(c => c.Ward).AsNoTracking().FirstOrDefaultAsync(c => c.Id.ToString() == _user.Id, cancellation);

            if (user == null) return new UserMapping();

            return _mapper.Map<UserMapping>(user);
        }

        public async Task<TokenRequest> RefreshAccessToken(string token, CancellationToken cancellation)
        {
            var refreshToken = await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(c => c.Token == token, cancellation);

            if (refreshToken == null || refreshToken.ExpiryTime < DateTime.UtcNow)
                return new TokenRequest();

            return new TokenRequest { AccessToken = GenerateAccessToken(refreshToken.UserId.ToString()), RefreshToken = refreshToken.Token };
        }

        public string GenerateAccessToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenRequest> Login(LoginRequest request, CancellationToken cancellation)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Email == request.Email, cancellation);

            if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                var refreshToken = GenerateRefreshToken();

                await _context.RefreshTokens.AddAsync(new RefreshToken()
                {
                    Token = refreshToken,
                    ExpiryTime = DateTime.Now.AddDays(30),
                    UserId = user.Id
                }, cancellation);

                await _context.SaveChangesAsync(cancellation);

                return new TokenRequest { AccessToken = GenerateAccessToken(user.Id.ToString()), RefreshToken = refreshToken };
            }

            return new TokenRequest();
        }

        public async Task<Result> Register(RegisterRequest request, CancellationToken cancellation)
        {
            if (_context.Users.Any(c => c.Username == request.Username || c.Email == request.Email))
            {
                return Result.Failure("Tên Người Dùng Hoặc Email Đã Tồn Tại");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.Users.AddAsync(new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.Username,
            }, cancellation);

            await _context.SaveChangesAsync(cancellation);

            return Result.Success();
        }

        private string GenerateEmailConfirmationToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
