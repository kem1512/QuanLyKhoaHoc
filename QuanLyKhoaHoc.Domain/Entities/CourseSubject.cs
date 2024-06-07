namespace QuanLyKhoaHoc.Domain.Entities
{
    public class CourseSubject
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public Course Course { get; set; } = default!;

        public Subject Subject { get; set; } = default!;
    }
}
