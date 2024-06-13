namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class RunTestCaseMapping
    {
        public int Id { get; set; }

        public int DoHomeworkId { get; set; }

        public int TestCaseId { get; set; }

        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }

    public class RunTestCaseCreate
    {
        public int DoHomeworkId { get; set; }

        public int TestCaseId { get; set; }

        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }

    public class RunTestCaseUpdate
    {
        public int Id { get; set; }

        public int DoHomeworkId { get; set; }

        public int TestCaseId { get; set; }

        public string Result { get; set; } = default!;

        public double RunTime { get; set; }
    }
}
