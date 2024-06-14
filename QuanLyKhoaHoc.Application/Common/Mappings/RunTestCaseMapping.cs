namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class RunTestCaseMapping
    {
        public int Id { get; set; }

        public int DoHomeworkId { get; set; }

        public int TestCaseId { get; set; }

        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }

    public class RunTestCaseCreate
    {
        [Required(ErrorMessage = "Bài Tập Về Nhà Không Được Bỏ Trống")]
        public int DoHomeworkId { get; set; }

        [Required(ErrorMessage = "Test Case Không Được Bỏ Trống")]
        public int TestCaseId { get; set; }

        [Required(ErrorMessage = "Kết Quả Không Được Bỏ Trống")]
        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }

    public class RunTestCaseUpdate
    {
        [Required(ErrorMessage = "Test Case Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bài Tập Về Nhà Không Được Bỏ Trống")]
        public int DoHomeworkId { get; set; }

        [Required(ErrorMessage = "Test Case Không Được Bỏ Trống")]
        public int TestCaseId { get; set; }

        [Required(ErrorMessage = "Kết Quả Không Được Bỏ Trống")]
        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }
}
