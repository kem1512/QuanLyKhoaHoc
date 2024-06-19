using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Common.Mapping
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

        public SubjectDetailMapping SubjectDetail { get; set; } = default!;

        public ProgramingLanguageMapping ProgramingLanguage { get; set; } = default!;
    }

    public class PracticeQuery : QueryModel { }

    public class PracticeCreate
    {
        [Required(ErrorMessage = "Chi Tiết Chủ Đề Không Được Bỏ Trống")]
        public int SubjectDetailId { get; set; }

        public Level Level { get; set; }

        [Required(ErrorMessage = "Mã Bài Tập Không Được Bỏ Trống")]
        public string PracticeCode { get; set; } = default!;

        [Required(ErrorMessage = "Tiêu Đề Không Được Bỏ Trống")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Chủ Đề Không Được Bỏ Trống")]
        public string Topic { get; set; } = default!;

        [Required(ErrorMessage = "Đầu Ra Không Được Bỏ Trống")]
        public string ExpectOutput { get; set; } = default!;

        [Required(ErrorMessage = "Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public int LanguageProgrammingId { get; set; }

        public bool IsRequired { get; set; }

        public bool IsDeleted { get; set; }

        public double MediumScore { get; set; }
    }

    public class PracticeUpdate
    {
        [Required(ErrorMessage = "Bài Tập Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Chi Tiết Chủ Đề Không Được Bỏ Trống")]
        public int SubjectDetailId { get; set; }

        public Level Level { get; set; }

        [Required(ErrorMessage = "Mã Bài Tập Không Được Bỏ Trống")]
        public string PracticeCode { get; set; } = default!;

        [Required(ErrorMessage = "Tiêu Đề Không Được Bỏ Trống")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Chủ Đề Không Được Bỏ Trống")]
        public string Topic { get; set; } = default!;

        [Required(ErrorMessage = "Đầu Ra Không Được Bỏ Trống")]
        public string ExpectOutput { get; set; } = default!;

        [Required(ErrorMessage = "Ngôn Ngữ Lập Trình Không Được Bỏ Trống")]
        public int LanguageProgrammingId { get; set; }

        public bool IsRequired { get; set; }

        public bool IsDeleted { get; set; }

        public double MediumScore { get; set; }
    }
}
