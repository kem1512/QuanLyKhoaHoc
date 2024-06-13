namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string[]? roles, string? certificate);
        string GenerateRefreshToken();
        string GenerateEmailConfirmationToken();
    }
}
