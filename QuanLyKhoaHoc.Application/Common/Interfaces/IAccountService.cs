namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<Result> UserInfoUpdate(UserInfoUpdate entity, CancellationToken cancellation);

        Task<UserInfo?> UserInfo(CancellationToken cancellation);

        Task<RegisterStudyMapping?> RegisterStudy(int courseId, CancellationToken cancellation);
    }
}
