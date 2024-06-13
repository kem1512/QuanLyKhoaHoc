namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class SubjectDetailMapping
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }
    }

    public class SubjectDetailQuery : QueryModel { }

    public class SubjectDetailCreate
    {
        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }
    }

    public class SubjectDetailUpdate
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = default!;

        public bool IsFinished { get; set; }

        public string LinkVideo { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
