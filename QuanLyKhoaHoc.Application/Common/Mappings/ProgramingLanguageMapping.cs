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
        [Required(ErrorMessage = "Tên Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public string LanguageName { get; set; } = default!;
    }

    public class ProgramingLanguageUpdate
    {
        [Required(ErrorMessage = "Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public string LanguageName { get; set; } = default!;
    }
}
