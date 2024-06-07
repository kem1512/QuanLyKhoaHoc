using QuanLyKhoaHoc.Domain;

namespace QuanLyKhoaHoc.Application.Common.Models;

public class Result
{
    internal Result(ResultStatus status, string? error)
    {
        Status = status;
        Error = error;
    }

    public ResultStatus Status { get; init; }

    public string? Error { get; init; }

    public static Result Success()
    {
        return new Result(ResultStatus.Succeess, null);
    }

    public static Result Failure(string errors = "Lỗi Gì Đó")
    {
        return new Result(ResultStatus.Failure, errors);
    }
}
