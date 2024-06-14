namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class RegisterStudyConfiguration : IEntityTypeConfiguration<RegisterStudy>
    {
        public void Configure(EntityTypeBuilder<RegisterStudy> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.RegisterTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.DoneTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.IsFinished).HasDefaultValue(false);

            builder.Property(c => c.IsActive).HasDefaultValue(false);

            builder.Property(c => c.PercentComplete).HasDefaultValue(0);

            builder.HasOne(c => c.User).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Course).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.CurrentSubject).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.CurrentSubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
