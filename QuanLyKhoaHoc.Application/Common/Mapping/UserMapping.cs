using QuanLyKhoaHoc.Domain;

namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class UserMapping
    {
        public int Id { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }

        public int? CertificateId { get; set; }

        public int? WardId { get; set; }

        public string Username { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public string? Avatar { get; set; } = default!;

        public string Email { get; set; } = default!;

        public DateTime UpdateTime { get; set; }

        public string FullName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public string? Address { get; set; } = default!;

        public UserStatus UserStatus { get; set; }

        public DistrictMapping District { get; set; } = default!;

        public ProvinceMapping Province { get; set; } = default!;

        public CertificateMapping Certificate { get; set; } = default!;

        public WardMapping Ward { get; set; } = default!;

        public ICollection<PermissionMapping> Permissions { get; set; } = default!;
    }

    public class UserQuery : QueryModel { }

    public class UserCreate
    {
        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }

        public int? CertificateId { get; set; }

        public int? WardId { get; set; }

        public string Username { get; set; } = default!;

        public string? Avatar { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public string? Address { get; set; } = default!;

        public string Password { get; set; } = default!;

        public UserStatus UserStatus { get; set; }

        public ICollection<PermissionMapping> Permissions { get; set; } = default!;
    }

    public class UserUpdate
    {
        public int Id { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }

        public int? CertificateId { get; set; }

        public int? WardId { get; set; }

        public string Username { get; set; } = default!;

        public string? Avatar { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public string? Address { get; set; } = default!;

        public UserStatus UserStatus { get; set; }

        public ICollection<PermissionMapping> Permissions { get; set; } = default!;
    }

    public class PermissionMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public RoleMapping Role { get; set; } = default!;
    }
}
