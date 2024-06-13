namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CommentBlogMapping
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int UserId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; } = default!;

        public bool Edited { get; set; }

        public User User { get; set; } = default!;
    }

    public class CommentBlogQuery : QueryModel {
        public int? BlogId { get; set; }
    }

    public class CommentBlogCreate
    {
        public int BlogId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; } = default!;
    }

    public class CommentBlogUpdate
    {
        public int Id { get; set; }

        public string Content { get; set; } = default!;
    }
}
