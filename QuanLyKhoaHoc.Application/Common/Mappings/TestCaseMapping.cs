namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class TestCaseMapping
    {
        public int Id { get; set; }

        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public int ProgramingLanguageId { get; set; }

        public int PracticeId { get; set; }
    }

    public class TestCaseQuery : QueryModel { }

    public class TestCaseCreate
    {
        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public int ProgramingLanguageId { get; set; }

        public int PracticeId { get; set; }
    }

    public class TestCaseUpdate
    {
        public int Id { get; set; }

        public string Input { get; set; } = default!;

        public string Output { get; set; } = default!;

        public int ProgramingLanguageId { get; set; }

        public int PracticeId { get; set; }
    }
}
