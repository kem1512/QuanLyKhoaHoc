using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CertificateTypeConfiguration : IEntityTypeConfiguration<CertificateType>
    {
        public void Configure(EntityTypeBuilder<CertificateType> builder)
        {
           
        }
    }
}
