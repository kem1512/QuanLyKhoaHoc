using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class BlogMapping
    {
        public int Id { get; set; }

        public string Content { get; set; } = default!;

        public string Image {  get; set; } = default!;

        public string Title { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public User Creator { get; set; } = default!;

        public ICollection<CommentBlogMapping> CommentBlogs { get; set; } = default!;

        public ICollection<LikeBlog> LikeBlogs { get; set; } = default!;
    }

    public class BlogQuery : QueryModel { }

    public class BlogCreate
    {
        public string Content { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Image { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class BlogUpdate
    {
        public int Id { get; set; }

        public string Content { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Image { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
