namespace QuanLyKhoaHoc.Domain.Entities
{
    public class RegisterStudy
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

        public User User { get; set; } = default!;

        public Course Course { get; set; } = default!;

        public Subject CurrentSubject { get; set; } = default!;

        public ICollection<DoHomework> DoHomeworks { get; set; } = default!;

        public ICollection<LearningProgress> LearningProgresses { get; set; } = default!;
    }
}
