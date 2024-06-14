namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class ConfirmEmailConfiguration : IEntityTypeConfiguration<ConfirmEmail>
    {
        public void Configure(EntityTypeBuilder<ConfirmEmail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsConfirm).HasDefaultValue(false);

            builder.HasIndex(c => c.ConfirmCode).IsUnique();

            builder.Property(c => c.ExpiryTime).HasDefaultValueSql("DATEADD(day, 30, GETDATE())");

            builder.HasOne(c => c.User).WithMany(c => c.ConfirmEmails).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
