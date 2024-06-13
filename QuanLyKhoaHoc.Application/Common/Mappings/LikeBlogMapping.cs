namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class LikeBlogMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public bool Unlike { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User User { get; set; } = default!;
    }

    public class LikeBlogQuery : QueryModel { }

    public class LikeBlogCreate
    {
        public int BlogId { get; set; }
    }

    public class LikeBlogUpdate
    {
        public int Id { get; set; }

        public int BlogId { get; set; }
    }
}
