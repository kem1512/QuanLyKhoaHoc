namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class BlogMapping
    {
        public int Id { get; set; }

        public string Content { get; set; } = default!;

        public string Image {  get; set; } = default!;

        public string Title { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public bool IsLiked { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public UserMapping Creator { get; set; } = default!;

        public ICollection<CommentBlogMapping> CommentBlogs { get; set; } = default!;

        public ICollection<LikeBlog> LikeBlogs { get; set; } = default!;
    }

    public class BlogQuery : QueryModel { }

    public class BlogCreate
    {
        [Required(ErrorMessage = "Nội Dung Không Được Bỏ Trống")]
        public string Content { get; set; } = default!;

        [Required(ErrorMessage = "Tiêu Đề Không Được Bỏ Trống")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Hình Ảnh Không Được Bỏ Trống")]
        public string Image { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class BlogUpdate
    {
        [Required(ErrorMessage = "Bài Viết Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nội Dung Không Được Bỏ Trống")]
        public string Content { get; set; } = default!;

        [Required(ErrorMessage = "Tiêu Đề Không Được Bỏ Trống")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Hình Ảnh Không Được Bỏ Trống")]
        public string Image { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
