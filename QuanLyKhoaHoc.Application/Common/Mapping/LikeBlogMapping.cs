namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class LikeBlogMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BlogId { get; set; }

        public bool Unlike { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public UserMapping User { get; set; } = default!;

        public BlogMapping Blog { get; set; } = default!;
    }

    public class LikeBlogQuery : QueryModel { }

    public class LikeBlogCreate
    {
        [Required(ErrorMessage = "Bài Viết Không Được Bỏ Trống")]
        public int BlogId { get; set; }
    }

    public class LikeBlogUpdate
    {
        [Required(ErrorMessage = "Bài Viết Đã Thích Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bài Viết Không Được Bỏ Trống")]
        public int BlogId { get; set; }
    }
}
