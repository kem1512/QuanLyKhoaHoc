namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Practice
    {
        public int Id { get; set; }

        public int SubjectDetailId { get; set; }

        public Level Level { get; set; }

        public string PracticeCode { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Topic { get; set; } = default!;

        public string ExpectOutput { get; set; } = default!;

        public int LanguageProgrammingId { get; set; }

        public bool IsRequired { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool IsDeleted { get; set; }

        public double MediumScore { get; set; }

        public SubjectDetail SubjectDetail { get; set; } = default!;

        public ProgramingLanguage ProgramingLanguage { get; set; } = default!;

        public ICollection<DoHomework> DoHomeworks { get; set; } = default!;

        public ICollection<TestCase> TestCases { get; set; } = default!;
    }
}
