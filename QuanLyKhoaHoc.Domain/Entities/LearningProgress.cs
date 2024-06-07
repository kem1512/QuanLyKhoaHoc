namespace QuanLyKhoaHoc.Domain.Entities
{
    public class LearningProgress
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RegisterStudyId { get; set; }

        public int CurrentSubjectId { get; set; }

        public User User { get; set; } = default!;

        public RegisterStudy RegisterStudy { get; set; } = default!;

        public Subject CurrentSubject { get; set; } = default!;
    }
}
