using QuanLyKhoaHoc.Domain;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class DoHomeworkConfiguration : IEntityTypeConfiguration<DoHomework>
    {
        public void Configure(EntityTypeBuilder<DoHomework> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsFinished).HasDefaultValue(false);

            builder.Property(c => c.HomeworkStatus).HasDefaultValue(HomeworkStatus.NotStarted);

            builder.HasOne(c => c.Practice).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.PracticeId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.User).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.RegisterStudy).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.RegisterStudyId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
