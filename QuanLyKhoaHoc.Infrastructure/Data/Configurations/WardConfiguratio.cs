namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.District).WithMany(c => c.Wards).HasForeignKey(c => c.DistrictId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
