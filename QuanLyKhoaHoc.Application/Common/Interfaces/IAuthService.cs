﻿namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<TokenRequest?> RefreshAccessToken(string refreshToken, CancellationToken cancellation);

        Task<TokenRequest?> Login(LoginRequest request, CancellationToken cancellation);

        Task<Result> Register(RegisterRequest request, CancellationToken cancellation);

        Task<Result> ConfirmEmail(string token, CancellationToken cancellation);

        Task<Result> SendConfirmEmail(string email, CancellationToken cancellation);

        Task Logout(string token, CancellationToken cancellation);
    }
}
