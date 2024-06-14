namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answers>
    {
        public void Configure(EntityTypeBuilder<Answers> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.User).WithMany(c => c.Answers).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Question).WithMany(c => c.Answers).HasForeignKey(c => c.QuestionId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
