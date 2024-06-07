using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Domain.Entities;

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
        if (!_context.Users.Any()) {
            var user = new User()
            {
                Username = "username",
                FullName = "Nguyễn Viết Hải Đăng",
                Email = "haidang15122002@gmail.com",
                Address = "Hà Nội",
                Avatar = "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-7.png",
                IsActive = true,
                Password = "123456",
                UserStatus = Domain.UserStatus.Active,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                DateOfBirth = DateTime.Now,
            };

            await _context.Users.AddAsync(user);
        }

        await _context.SaveChangesAsync();
    }
}
