using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class ProgramingLanguageConfiguration : IEntityTypeConfiguration<ProgramingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgramingLanguage> builder)
        {

        }
    }
}
