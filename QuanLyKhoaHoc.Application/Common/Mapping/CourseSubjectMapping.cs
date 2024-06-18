namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class CourseSubjectMapping
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public SubjectMapping? Subject { get; set; }
    }
}
