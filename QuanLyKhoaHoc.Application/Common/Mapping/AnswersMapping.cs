namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class AnswersMapping
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Answer { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public MakeQuestion Question { get; set; } = default!;

        public UserMapping User { get; set; } = default!;
    }

    public class AnswersQuery : QueryModel
    {
        public int? QuestionId { get; set; }
    }

    public class AnswersCreate
    {
        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Answer { get; set; } = default!;
    }

    public class AnswersUpdate
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Answer { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
