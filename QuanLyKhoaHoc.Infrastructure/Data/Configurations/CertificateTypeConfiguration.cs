namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class CertificateTypeConfiguration : IEntityTypeConfiguration<CertificateType>
    {
        public void Configure(EntityTypeBuilder<CertificateType> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
