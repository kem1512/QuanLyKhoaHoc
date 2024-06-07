using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.District).WithMany(c => c.Users).HasForeignKey(c => c.DistrictId);

            builder.HasOne(c => c.Province).WithMany(c => c.Users).HasForeignKey(c => c.ProvinceId);

            builder.HasOne(c => c.Certificate).WithMany(c => c.Users).HasForeignKey(c => c.CertificateId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Ward).WithMany(c => c.Users).HasForeignKey(c => c.WardId);
        }
    }
}
