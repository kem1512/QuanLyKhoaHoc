using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class WardMapping
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;
    }

    public class WardQuery : QueryModel { }

    public class WardCreate
    {
        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;
    }

    public class WardUpdate
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;
    }
}
