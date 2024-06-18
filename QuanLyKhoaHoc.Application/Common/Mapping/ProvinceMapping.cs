namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class ProvinceMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    public class ProvinceQuery : QueryModel { }

    public class ProvinceCreate
    {
        [Required(ErrorMessage = "Tên Tỉnh / Thành Phố Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }

    public class ProvinceUpdate
    {
        [Required(ErrorMessage = "Tỉnh / Thành Phố Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Tỉnh / Thành Phố Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }
}
