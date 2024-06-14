namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class SubjectDetailConfiguration : IEntityTypeConfiguration<SubjectDetail>
    {
        public void Configure(EntityTypeBuilder<SubjectDetail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.IsFinished).HasDefaultValue(true);

            builder.Property(c => c.IsActive).HasDefaultValue(true);

            builder.HasOne(c => c.Subject).WithMany(c => c.SubjectDetails).HasForeignKey(c => c.SubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
