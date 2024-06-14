namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ExpiryTime).HasDefaultValueSql("DATEADD(day, 30, GETDATE())");

            builder.HasOne(c => c.User).WithMany(c => c.RefreshTokens).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
