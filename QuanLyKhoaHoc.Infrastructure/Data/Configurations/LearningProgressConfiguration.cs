using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class LearningProgressConfiguration : IEntityTypeConfiguration<LearningProgress>
    {
        public void Configure(EntityTypeBuilder<LearningProgress> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.LearningProgresses).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.RegisterStudy).WithMany(c => c.LearningProgresses).HasForeignKey(c => c.RegisterStudyId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.CurrentSubject).WithMany(c => c.LearningProgresses).HasForeignKey(c => c.CurrentSubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
