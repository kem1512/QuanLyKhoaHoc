namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class CourseMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Introduce { get; set; } = default!;

        public string ImageCourse { get; set; } = default!;

        public int CreatorId { get; set; }

        public string Code { get; set; } = default!;

        public decimal Price { get; set; }

        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }

        public UserMapping Creator { get; set; } = default!;
    }

    public class CourseQuery : QueryModel { }

    public class CourseCreate
    {
        [Required(ErrorMessage = "Tên Khóa Học Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Mô Tả Không Được Bỏ Trống")]
        public string Introduce { get; set; } = default!;

        [Required(ErrorMessage = "Ảnh Khóa Học Không Được Bỏ Trống")]
        public string ImageCourse { get; set; } = default!;

        [Required(ErrorMessage = "Mã Khóa Học Không Được Bỏ Trống")]
        public string Code { get; set; } = default!;

        [Required(ErrorMessage = "Giá Không Được Bỏ Trống")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Thời Gian Học Không Được Bỏ Trống")]
        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }

        public ICollection<CourseSubjectMapping> CourseSubjects { get; set; } = default!;
    }

    public class CourseUpdate
    {
        [Required(ErrorMessage = "Khóa Học Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Khóa Học Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Mô Tả Không Được Bỏ Trống")]
        public string Introduce { get; set; } = default!;

        [Required(ErrorMessage = "Ảnh Khóa Học Không Được Bỏ Trống")]
        public string ImageCourse { get; set; } = default!;

        [Required(ErrorMessage = "Mã Khóa Học Không Được Bỏ Trống")]
        public string Code { get; set; } = default!;

        [Required(ErrorMessage = "Giá Không Được Bỏ Trống")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Thời Gian Học Không Được Bỏ Trống")]
        public int TotalCourseDuration { get; set; }

        public int NumberOfStudent { get; set; }

        public int NumberOfPurchases { get; set; }

        public ICollection<CourseSubjectMapping> CourseSubjects { get; set; } = default!;
    }
}
