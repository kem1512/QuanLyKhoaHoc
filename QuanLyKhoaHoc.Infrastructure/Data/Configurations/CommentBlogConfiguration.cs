namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CommentBlogConfiguration : IEntityTypeConfiguration<CommentBlog>
    {
        public void Configure(EntityTypeBuilder<CommentBlog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ReplyCount).HasDefaultValue(0);

            builder.Property(c => c.Edited).HasDefaultValue(false);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.Blog).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.User).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Parent).WithMany(c => c.Childs).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
