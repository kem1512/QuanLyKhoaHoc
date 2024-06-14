namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
