namespace QuanLyKhoaHoc.Domain.Entities
{
    public class DoHomeworkMapping
    {
        public int Id { get; set; }
        
        public int PracticeId { get; set; }

        public int UserId { get; set; }

        public HomeworkStatus HomeworkStatus { get; set; }

        public bool IsFinished { get; set; }

        public string ActualOutput { get; set; } = string.Empty;

        public DateTime? DoneTime { get; set; }

        public int RegisterStudyId { get; set; }

        public ICollection<RunTestCaseMapping> TestCases { get; set; } = default!;
    }

    public class DoHomeworkQuery : QueryModel{
        public int? PracticeId { get; set; }
    }

    public class DoHomeworkCreate
    {
        public int PracticeId { get; set; }

        public int UserId { get; set; }

        public HomeworkStatus HomeworkStatus { get; set; }

        public bool IsFinished { get; set; }

        public string ActualOutput { get; set; } = string.Empty;

        public DateTime? DoneTime { get; set; }

        public int RegisterStudyId { get; set; }
    }

    public class DoHomeworkUpdate
    {
        public int Id { get; set; }

        public int PracticeId { get; set; }

        public int UserId { get; set; }

        public HomeworkStatus HomeworkStatus { get; set; }

        public bool IsFinished { get; set; }

        public string ActualOutput { get; set; } = string.Empty;

        public DateTime? DoneTime { get; set; }

        public int RegisterStudyId { get; set; }

        public ICollection<RunTestCaseMapping> TestCases { get; set; } = default!;
    }
}
