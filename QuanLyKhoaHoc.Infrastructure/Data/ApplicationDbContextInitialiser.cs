namespace QuanLyKhoaHoc.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser( ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var administratorRole = new Role() { RoleName = Roles.Administrator, RoleCode = "administrator" };

        var instructorCertificate = new CertificateType() { Name = "Chứng Chỉ Hệ Thống", Certificates = new List<Certificate>() { new Certificate() { Name = "Chứng Chỉ Giảng Viên", Description = "Thêm Khóa Học Cho Hệ Thống", Image = "/images/1.png" } } };

        if (!instructorCertificate.Certificates.Any(c => c.Name == Certificates.InstructorCertificate))
        {
            await _context.CertificateTypes.AddAsync(instructorCertificate);
        }

        var administrator = new User()
        {
            Username = "admin@khoahocviet.nguyenviethaidang.id.vn",
            FullName = "Nguyễn Viết Hải Đăng",
            Email = "admin@khoahocviet.nguyenviethaidang.id.vn",
            Address = "Hà Nội",
            Avatar = "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-7.png",
            IsActive = true,
            Password = BCrypt.Net.BCrypt.HashPassword("toidaidot1512"),
            UserStatus = Domain.UserStatus.Active,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            DateOfBirth = DateTime.Now,
            Permissions = new List<Permission>()
                {
                    new Permission(){ Role = new Role(){ RoleName = "Administrator", RoleCode = "administrator" }}
                }
        };

        if (_context.Users.All(c => c.Email != administrator.Email)) {
            await _context.Users.AddAsync(administrator);
        }

        await _context.SaveChangesAsync();
    }
}
