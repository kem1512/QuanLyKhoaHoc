using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CommentBlogConfiguration : IEntityTypeConfiguration<CommentBlog>
    {
        public void Configure(EntityTypeBuilder<CommentBlog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content).HasMaxLength(123);

            builder.HasOne(c => c.Blog).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.User).WithMany(c => c.CommentBlogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
