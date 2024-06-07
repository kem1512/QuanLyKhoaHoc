namespace QuanLyKhoaHoc.Domain.Entities
{
    public class RunTestCase
    {
        public int Id { get; set; }

        public int DoHomeworkId { get; set; }

        public int TestCaseId { get; set; }

        public string Result { get; set; } = default!;

        public double RunTime { get; set; }

        public DoHomework DoHomework { get; set; } = default!;

        public TestCase TestCase { get; set; } = default!;
    }
}
