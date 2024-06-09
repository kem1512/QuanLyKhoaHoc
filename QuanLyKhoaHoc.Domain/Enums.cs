namespace QuanLyKhoaHoc.Domain
{
    public enum UserStatus
    {
        Active,
        Inactive,
        Banned,
        Pending
    }

    public enum Level
    {
        Beginner,
        Intermediate,
        Advanced,
        Expert
    }

    public enum HomeworkStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Overdue
    }

    public enum ResultStatus
    {
        Success,
        Failure,
        NotFound,
        Forbidden
    }
}