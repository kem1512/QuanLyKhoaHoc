namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; } = default!;

        public string Issuer { get; set; } = default!;

        public string Audience { get; set; } = default!;

        public int AccessTokenExpirationMinutes { get; set; }

        public int RefreshTokenExpirationDays { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }

    public class TokenRequest
    {
        public string AccessToken { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }

    public class EmailSettings
    {
        public string Host { get; set; } = default!;

        public int Port { get; set; } = default!;

        public string Username { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool EnableSsl { get; set; } = default!;
    }
}
