namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsSeen).HasDefaultValue(false);

            builder.HasOne(c => c.User).WithMany(c => c.Notifications).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
