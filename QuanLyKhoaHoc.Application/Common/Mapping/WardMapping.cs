using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class WardMapping
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;

        public DistrictMapping District { get; set; } = default!;
    }

    public class WardQuery : QueryModel
    {
        public int? DistrictId { get; set; }
    }

    public class WardCreate
    {
        [Required(ErrorMessage = "Quận / Huyện Không Được Bỏ Trống")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Tên Thị / Xã Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }

    public class WardUpdate
    {
        [Required(ErrorMessage = "Thị / Xã Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quận / Huyện Không Được Bỏ Trống")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Tên Thị / Xã Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }
}
