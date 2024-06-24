namespace QuanLyKhoaHoc.Application.Common.Mapping
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
        [Required(ErrorMessage = "Mã Vai Trò Không Được Bỏ Trống")]
        public string RoleCode { get; set; } = default!;

        [Required(ErrorMessage = "Tên Vai Trò Không Được Bỏ Trống")]
        public string RoleName { get; set; } = default!;
    }

    public class RoleUpdate
    {
        [Required(ErrorMessage = "Vai Trò Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã Vai Trò Không Được Bỏ Trống")]
        public string RoleCode { get; set; } = default!;

        [Required(ErrorMessage = "Tên Vai Trò Không Được Bỏ Trống")]
        public string RoleName { get; set; } = default!;
    }
}
