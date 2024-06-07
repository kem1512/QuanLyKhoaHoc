using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class DoHomeworkConfiguration : IEntityTypeConfiguration<DoHomework>
    {
        public void Configure(EntityTypeBuilder<DoHomework> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Practice).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.PracticeId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.User).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.RegisterStudy).WithMany(c => c.DoHomeworks).HasForeignKey(c => c.RegisterStudyId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
