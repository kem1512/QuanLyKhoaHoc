namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(c => c.RoleName).IsUnique();

            builder.HasIndex(c => c.RoleCode).IsUnique();
        }
    }
}
