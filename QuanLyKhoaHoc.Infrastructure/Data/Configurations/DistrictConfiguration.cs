using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Province).WithMany(c => c.Districts).HasForeignKey(c => c.ProvinceId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
