namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class MakeQuestionConfiguration : IEntityTypeConfiguration<MakeQuestion>
    {
        public void Configure(EntityTypeBuilder<MakeQuestion> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NumberOfAnswers).HasDefaultValue(0);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.User).WithMany(c => c.MakeQuestions).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.SubjectDetail).WithMany(c => c.MakeQuestions).HasForeignKey(c => c.SubjectDetailId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
