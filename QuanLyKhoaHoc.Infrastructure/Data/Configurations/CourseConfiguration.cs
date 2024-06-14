namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NumberOfPurchases).HasDefaultValue(0);

            builder.Property(c => c.NumberOfStudent).HasDefaultValue(0);

            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(c => c.TotalCourseDuration).HasDefaultValue(0);

            builder.Property(c => c.Price).HasColumnType("DECIMAL(18, 2)");

            builder.HasOne(c => c.Creator).WithMany(c => c.Courses).HasForeignKey(c => c.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
