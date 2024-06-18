namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class DistrictMapping
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string Name { get; set; } = default!;
    }

    public class DistrictQuery : QueryModel
    {
        public int? ProvinceId { get; set; }
    }

    public class DistrictCreate
    {
        [Required(ErrorMessage = "Quận / Huyện Không Được Bỏ Trống")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Tên Quận / Huyện Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }

    public class DistrictUpdate
    {
        [Required(ErrorMessage = "Quận / Huyện Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quận / Huyện Không Được Bỏ Trống")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Tên Quận / Huyện Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }
}
