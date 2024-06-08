using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class DistrictMapping
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string Name { get; set; } = default!;
    }

    public class DistrictQuery : QueryModel { }

    public class DistrictCreate
    {
        public int ProvinceId { get; set; }

        public string Name { get; set; } = default!;
    }

    public class DistrictUpdate
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string Name { get; set; } = default!;
    }
}
