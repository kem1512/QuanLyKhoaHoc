using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class LikeBlogConfiguration : IEntityTypeConfiguration<LikeBlog>
    {
        public void Configure(EntityTypeBuilder<LikeBlog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.LikeBlogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Blog).WithMany(c => c.LikeBlogs).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
