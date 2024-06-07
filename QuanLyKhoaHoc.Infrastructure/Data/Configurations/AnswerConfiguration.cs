using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answers>
    {
        public void Configure(EntityTypeBuilder<Answers> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.Answers).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Question).WithMany(c => c.Answers).HasForeignKey(c => c.QuestionId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
