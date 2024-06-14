namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class ProgramingLanguageConfiguration : IEntityTypeConfiguration<ProgramingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgramingLanguage> builder)
        {
            builder.HasIndex(c => c.LanguageName).IsUnique();
        }
    }
}
