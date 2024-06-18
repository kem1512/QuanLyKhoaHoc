namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class BillStatusMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    public class BillStatusQuery : QueryModel { }

    public class BillStatusCreate
    {
        public string Name { get; set; } = default!;
    }

    public class BillStatusUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
