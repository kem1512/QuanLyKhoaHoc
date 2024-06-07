using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class SubjectDetailConfiguration : IEntityTypeConfiguration<SubjectDetail>
    {
        public void Configure(EntityTypeBuilder<SubjectDetail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Subject).WithMany(c => c.SubjectDetails).HasForeignKey(c => c.SubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
