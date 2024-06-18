namespace QuanLyKhoaHoc.Application.Common.Models;

public class Result
{
    internal Result(ResultStatus status, string? error, string? content = null)
    {
        Status = status;
        Error = error;
        Content = content;
    }

    public ResultStatus Status { get; init; }

    public string? Error { get; init; }

    public string? Content { get; init; }

    public static Result Success(string? content = null)
    {
        return new Result(ResultStatus.Success, null, content);
    }

    public static Result Failure(string errors = "Lỗi Gì Đó")
    {
        return new Result(ResultStatus.Failure, errors);
    }
}
