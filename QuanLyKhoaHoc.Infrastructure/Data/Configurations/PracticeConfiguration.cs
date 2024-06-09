namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.SubjectDetail).WithMany(c => c.Practices).HasForeignKey(c => c.SubjectDetailId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.ProgramingLanguage).WithMany(c => c.Practices).HasForeignKey(c => c.LanguageProgrammingId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
