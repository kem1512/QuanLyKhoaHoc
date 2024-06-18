namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class LikeBlogConfiguration : IEntityTypeConfiguration<LikeBlog>
    {
        public void Configure(EntityTypeBuilder<LikeBlog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Unlike).HasDefaultValue(false);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(c => new { c.UserId, c.BlogId }).IsUnique();

            builder.HasOne(c => c.User).WithMany(c => c.LikeBlogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Blog).WithMany(c => c.LikeBlogs).HasForeignKey(c => c.BlogId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
