namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class ProgramingLanguageMapping
    {
        public int Id { get; set; }

        public string LanguageName { get; set; } = default!;
    }

    public class ProgramingLanguageQuery : QueryModel { }

    public class ProgramingLanguageCreate
    {
        public string LanguageName { get; set; } = default!;
    }

    public class ProgramingLanguageUpdate
    {
        public int Id { get; set; }

        public string LanguageName { get; set; } = default!;
    }
}
