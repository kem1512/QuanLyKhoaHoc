using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class BillStatusConfiguration : IEntityTypeConfiguration<BillStatus>
    {
        public void Configure(EntityTypeBuilder<BillStatus> builder)
        {
            
        }
    }
}
