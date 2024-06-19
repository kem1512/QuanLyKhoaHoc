namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class MakeQuestionMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;

        public int NumberOfAnswers { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public UserMapping User { get; set; } = default!;
    }

    public class MakeQuestionQuery : QueryModel { }

    public class MakeQuestionCreate
    {
        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;

        public int NumberOfAnswers { get; set; }
    }

    public class MakeQuestionUpdate
    {
        public int Id { get; set; }

        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;

        public int NumberOfAnswers { get; set; }
    }
}
