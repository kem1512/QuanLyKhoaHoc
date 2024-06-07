using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<TokenRequest> RefreshAccessToken(string refreshToken, CancellationToken cancellation);

        Task<TokenRequest> Login(LoginRequest request, CancellationToken cancellation);

        Task<Result> Register(RegisterRequest request, CancellationToken cancellation);

        Task<UserInfo> UserInfo(CancellationToken cancellation);
    }
}
