using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<TokenRequest?> RefreshAccessToken(string refreshToken, CancellationToken cancellation);

        Task<TokenRequest?> Login(LoginRequest request, CancellationToken cancellation);

        Task<Result> Register(RegisterRequest request, CancellationToken cancellation);

        Task<UserMapping?> UserInfo(CancellationToken cancellation);
    }
}
