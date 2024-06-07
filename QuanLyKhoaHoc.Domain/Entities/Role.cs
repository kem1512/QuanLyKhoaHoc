namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleCode { get; set; } = default!;

        public string RoleName { get; set; } = default!;

        public ICollection<Permission> Permissions { get; set; } = default!;
    }
}
