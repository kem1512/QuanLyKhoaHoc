﻿namespace QuanLyKhoaHoc.Domain.Entities
{
    public class CommentBlog
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int UserId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; } = default!;

        public bool Edited { get; set; }

        public int ReplyCount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public Blog Blog { get; set; } = default!;

        public User User { get; set; } = default!;

        public CommentBlog Parent { get; set; } = default!;

        public ICollection<CommentBlog> Childs { get; set; } = default!;
    }
}
