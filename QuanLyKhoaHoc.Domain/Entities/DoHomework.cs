namespace QuanLyKhoaHoc.Domain.Entities
{
    public class DoHomework
    {
        public int Id { get; set; }
        
        public int PracticeId { get; set; }

        public int UserId { get; set; }

        public HomeworkStatus HomeworkStatus { get; set; }

        public bool IsFinished { get; set; }

        public string ActualOutput { get; set; } = string.Empty;

        public DateTime DoneTime { get; set; }

        public int RegisterStudyId { get; set; }

        public Practice Practice { get; set; } = default!;

        public User User { get; set; } = default!;

        public RegisterStudy RegisterStudy { get; set; } = default!;

        public ICollection<RunTestCase> RunTestCases { get; set; } = default!;
    }
}
