namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CommentBlogConfiguration : IEntityTypeConfiguration<CommentBlog>
    {
        public void Configure(EntityTypeBuilder<CommentBlog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Blog).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.User).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Parent).WithMany(c => c.Childs).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
