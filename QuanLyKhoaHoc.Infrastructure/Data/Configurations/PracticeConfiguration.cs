using QuanLyKhoaHoc.Domain;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Level).HasDefaultValue(Level.Beginner);

            builder.Property(c => c.IsRequired).HasDefaultValue(true);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

            builder.Property(c => c.MediumScore).HasDefaultValue(0);

            builder.Property(c => c.CreateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.SubjectDetail).WithMany(c => c.Practices).HasForeignKey(c => c.SubjectDetailId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.ProgramingLanguage).WithMany(c => c.Practices).HasForeignKey(c => c.LanguageProgrammingId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
