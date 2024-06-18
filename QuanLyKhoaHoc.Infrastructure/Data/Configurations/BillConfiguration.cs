namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.TradingCode).IsUnique();

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.Price).HasColumnType("DECIMAL(18, 2)");

            builder.HasIndex(c => new { c.CourseId, c.UserId }).IsUnique();

            builder.HasOne(c => c.User).WithMany(c => c.Bills).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Course).WithMany(c => c.Bills).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.BillStatus).WithMany(c => c.Bills).HasForeignKey(c => c.BillStatusId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
