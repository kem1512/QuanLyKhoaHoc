using QuanLyKhoaHoc.Domain;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class PracticeMapping
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
    }

    public class PracticeQuery : QueryModel { }

    public class PracticeCreate
    {
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
    }

    public class PracticeUpdate
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
    }
}
