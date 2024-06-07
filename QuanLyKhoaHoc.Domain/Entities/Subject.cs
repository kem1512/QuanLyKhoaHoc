namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }

        public ICollection<CourseSubject> CourseSubjects { get; set; } = default!;

        public ICollection<LearningProgress> LearningProgresses { get; set; } = default!;

        public ICollection<RegisterStudy> RegisterStudies { get; set; } = default!;

        public ICollection<SubjectDetail> SubjectDetails { get; set; } = default!;
    }
}
