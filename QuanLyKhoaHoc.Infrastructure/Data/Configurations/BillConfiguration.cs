namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.Price).HasColumnType("DECIMAL(18, 2)");

            builder.HasOne(c => c.User).WithMany(c => c.Bills).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Course).WithMany(c => c.Bills).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
