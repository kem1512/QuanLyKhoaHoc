namespace QuanLyKhoaHoc.Domain.Entities
{
    public class LikeBlog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public bool Unlike { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User User { get; set; } = default!;

        public Blog Blog { get; set; } = default!;
    }
}
