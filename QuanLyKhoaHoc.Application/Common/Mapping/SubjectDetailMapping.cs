namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class SubjectDetailMapping
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }

        public SubjectMapping Subject { get; set; } = default!;

        public 
    }

    public class SubjectDetailQuery : QueryModel { }

    public class SubjectDetailCreate
    {
        [Required(ErrorMessage = "Chủ Đề Không Được Bỏ Trống")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Tên Chủ Đề Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        [Required(ErrorMessage = "Video Được Bỏ Trống")]
        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }
    }

    public class SubjectDetailUpdate
    {
        [Required(ErrorMessage = "Chi Tiết Chủ Đề Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Chủ Đề Không Được Bỏ Trống")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Tên Chủ Đề Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        [Required(ErrorMessage = "Video Được Bỏ Trống")]
        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
