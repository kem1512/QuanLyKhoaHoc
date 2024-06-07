using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.CertificateType).WithMany(c => c.Certificates).HasForeignKey(c => c.CertificateTypeId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
