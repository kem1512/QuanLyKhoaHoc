namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class CourseSubjectMapping
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public CourseMapping? Course { get; set; } = default!;

        public SubjectMapping Subject { get; set; } = default!;
    }

    public class CourseSubjectQuery : QueryModel 
    {
        public int? CourseId { get; set; }
    }

    public class CourseSubjectCreate
    {
        public int CourseId { get; set; }

        public int SubjectId { get; set; }
    }

    public class CourseSubjectUpdate
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }
    }
}
