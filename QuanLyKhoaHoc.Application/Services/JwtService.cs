using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyKhoaHoc.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public JwtService(IOptions<JwtSettings> jwtSettings, IApplicationDbContext context, IUser user)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
            _user = user;
        }

        public async Task<UserInfo> UserInfo(CancellationToken cancellation)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id.ToString() == _user.Id, cancellation);

            if (user == null) return new UserInfo();

            return new UserInfo() { Name = user.FullName, Email = user.Email, Avatar = user.Avatar };
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
                var newRefreshToken = GenerateRefreshToken();
                var refreshTokenExpiryTime = DateTime.Now.AddDays(30);

                var rf = await _context.RefreshTokens.FirstOrDefaultAsync(c => c.UserId == user.Id, cancellation);

                if (rf != null)
                {
                    rf.Token = newRefreshToken;
                    rf.ExpiryTime = refreshTokenExpiryTime;
                }
                else
                {
                    var refreshToken = new RefreshToken()
                    {
                        Token = newRefreshToken,
                        ExpiryTime = refreshTokenExpiryTime,
                        UserId = user.Id
                    };
                    await _context.RefreshTokens.AddAsync(refreshToken, cancellation);
                }

                await _context.SaveChangesAsync(cancellation);

                return new TokenRequest { AccessToken = GenerateAccessToken(user.Id.ToString()), RefreshToken = newRefreshToken };
            }

            return new TokenRequest();
        }

        public async Task<Result> Register(RegisterRequest request, CancellationToken cancellation)
        {
            if (IsUsernameOrEmailTaken(request.Username, request.Email))
            {
                return Result.Failure("Tên Người Dùng Hoặc Email Đã Tồn Tại");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = CreateUser(request, hashedPassword);

            await _context.Users.AddAsync(user, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return Result.Success();
        }

        private bool IsUsernameOrEmailTaken(string username, string email)
        {
            return _context.Users.Any(c => c.Username == username || c.Email == email);
        }

        private User CreateUser(RegisterRequest request, string hashedPassword)
        {
            return new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.Username,
                Address = "Địa Ngục",
                Avatar = "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-7.png",
                IsActive = true,
                UserStatus = Domain.UserStatus.Active,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                DateOfBirth = DateTime.Now,
            };
        }
    }
}
