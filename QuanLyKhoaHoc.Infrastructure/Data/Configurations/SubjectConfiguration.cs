namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.IsActive).HasDefaultValue(true);
        }
    }
}
