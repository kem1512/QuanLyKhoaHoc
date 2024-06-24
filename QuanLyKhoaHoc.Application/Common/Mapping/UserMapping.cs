namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class UserMapping
    {
        public int Id { get; set; }

        public string Username { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public string Email { get; set; } = default!;

        public DateTime UpdateTime { get; set; }

        public string FullName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public UserStatus UserStatus { get; set; }

        public CertificateMapping Certificate { get; set; } = default!;

        public ICollection<RegisterStudyMapping> RegisterStudies { get; set; } = default!;
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
