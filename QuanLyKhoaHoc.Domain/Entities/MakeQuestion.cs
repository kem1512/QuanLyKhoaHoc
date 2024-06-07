namespace QuanLyKhoaHoc.Domain.Entities
{
    public class MakeQuestion
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SubjectDetailId { get; set; }

        public string Question { get; set; } = default!;

        public int NumberOfAnswers { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User User { get; set; } = default!;

        public SubjectDetail SubjectDetail { get; set; } = default!;

        public ICollection<Answers> Answers { get; set; } = default!;
    }
}
