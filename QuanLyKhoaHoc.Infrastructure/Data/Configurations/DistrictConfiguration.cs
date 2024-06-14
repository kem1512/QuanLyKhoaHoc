namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasOne(c => c.Province).WithMany(c => c.Districts).HasForeignKey(c => c.ProvinceId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
