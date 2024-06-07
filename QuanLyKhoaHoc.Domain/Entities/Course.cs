namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Course
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

        public User Creator { get; set; } = default!;

        public ICollection<Bill> Bills { get; set; } = default!;

        public ICollection<CourseSubject> CourseSubjects { get; set; } = default!;

        public ICollection<RegisterStudy> RegisterStudies { get; set; } = default!;
    }
}
