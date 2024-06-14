namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Username).IsUnique();

            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.IsActive).HasDefaultValue(false);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.District).WithMany(c => c.Users).HasForeignKey(c => c.DistrictId);

            builder.HasOne(c => c.Province).WithMany(c => c.Users).HasForeignKey(c => c.ProvinceId);

            builder.HasOne(c => c.Certificate).WithMany(c => c.Users).HasForeignKey(c => c.CertificateId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Ward).WithMany(c => c.Users).HasForeignKey(c => c.WardId);
        }
    }
}
