namespace QuanLyKhoaHoc.Application.Common.Mapping
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
        [Required(ErrorMessage = "Input Không Được Bỏ Trống")]
        public string Input { get; set; } = default!;

        [Required(ErrorMessage = "Output Không Được Bỏ Trống")]
        public string Output { get; set; } = default!;

        [Required(ErrorMessage = "Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public int ProgramingLanguageId { get; set; }

        [Required(ErrorMessage = "Bài Tập Không Được Bỏ Trống")]
        public int PracticeId { get; set; }
    }

    public class TestCaseUpdate
    {
        [Required(ErrorMessage = "Test Case Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Input Không Được Bỏ Trống")]
        public string Input { get; set; } = default!;

        [Required(ErrorMessage = "Output Không Được Bỏ Trống")]
        public string Output { get; set; } = default!;

        [Required(ErrorMessage = "Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public int ProgramingLanguageId { get; set; }

        [Required(ErrorMessage = "Bài Tập Không Được Bỏ Trống")]
        public int PracticeId { get; set; }
    }
}
