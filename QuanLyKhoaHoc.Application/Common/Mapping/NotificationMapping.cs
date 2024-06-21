namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class NotificationMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Image { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Link { get; set; } = default!;

        public bool IsSeen { get; set; }
    }

    public class NotificationQuery : QueryModel { }

    public class NotificationCreate
    {
        public int UserId { get; set; }

        public string Image { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Link { get; set; } = default!;
    }

    public class NotificationUpdate
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Image { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Link { get; set; } = default!;

        public bool IsSeen { get; set; }
    }
}
