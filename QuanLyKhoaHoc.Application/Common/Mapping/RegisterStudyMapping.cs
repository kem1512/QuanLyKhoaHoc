namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class RegisterStudyMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public int CurrentSubjectId { get; set; }

        public bool IsFinished { get; set; }

        public DateTime RegisterTime { get; set; }

        public int PercentComplete { get; set; }

        public DateTime? DoneTime { get; set; }

        public bool IsActive { get; set; }
    }

    public class RegisterStudyQuery : QueryModel { }

    public class RegisterStudyCreate
    {
        [Required(ErrorMessage = "Người Dùng Không Được Bỏ Trống")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Khóa Học Không Được Bỏ Trống")]
        public int CourseId { get; set; }
    }

    public class RegisterStudyUpdate
    {
        [Required(ErrorMessage = "Đăng Ký Học Không Được Bỏ Trống")]
        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public int PercentComplete { get; set; }

        public bool IsActive { get; set; }
    }
}
