namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string[]? roles);
        string GenerateRefreshToken();
        string GenerateEmailConfirmationToken();
    }
}
