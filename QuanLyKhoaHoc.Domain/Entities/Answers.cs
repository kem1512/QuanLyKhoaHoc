namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Answers
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Answer { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public MakeQuestion Question { get; set; } = default!;

        public User User { get; set; } = default!;
    }
}
