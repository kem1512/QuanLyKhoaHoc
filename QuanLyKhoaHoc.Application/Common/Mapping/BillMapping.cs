namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class BillMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public decimal Price { get; set; }

        public string TradingCode { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public int BillStatusId { get; set; }

        public BillStatusMapping BillStatus { get; set; } = default!;

        public UserMapping User { get; set; } = default!;

        public CourseMapping Course { get; set; } = default!;
    }

    public class BillQuery : QueryModel { }

    public class BillCreate
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }

        public decimal Price { get; set; }

        public int BillStatusId { get; set; }

        public VNPaySettings? VNPaySettings { get; set; } = default!;
    }

    public class BillUpdate
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public decimal Price { get; set; }

        public string TradingCode { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public int BillStatusId { get; set; }
    }
}
