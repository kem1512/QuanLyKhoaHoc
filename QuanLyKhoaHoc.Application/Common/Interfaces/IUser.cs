namespace QuanLyKhoaHoc.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }

    bool IsAdministrator { get; }

    bool IsInstructorCertificate { get; }
}
