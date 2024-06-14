namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NumberOfComments).HasDefaultValue(0);

            builder.Property(c => c.NumberOfLikes).HasDefaultValue(0);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.Creator).WithMany(c => c.Blogs).HasForeignKey(c => c.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
