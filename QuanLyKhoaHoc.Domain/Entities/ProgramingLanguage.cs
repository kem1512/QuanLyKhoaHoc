namespace QuanLyKhoaHoc.Domain.Entities
{
    public class ProgramingLanguage
    {
        public int Id { get; set; }

        public string LanguageName { get; set; } = default!;

        public ICollection<Practice> Practices { get; set; } = default!;

        public ICollection<TestCase> TestCases { get; set; } = default!;
    }
}
