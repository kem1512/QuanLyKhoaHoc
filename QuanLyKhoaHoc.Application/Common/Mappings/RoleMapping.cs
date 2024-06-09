using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class RoleMapping
    {
        public int Id { get; set; }

        public string RoleCode { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }

    public class RoleQuery : QueryModel { }

    public class RoleCreate
    {
        public string RoleCode { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }

    public class RoleUpdate
    {
        public int Id { get; set; }

        public string RoleCode { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }
}
