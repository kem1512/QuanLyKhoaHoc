namespace QuanLyKhoaHoc.Application.Common.Mappings
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

    public class RegisterStudyUpdate
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
}
