using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class ProvinceMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    public class ProvinceQuery : QueryModel { }

    public class ProvinceCreate
    {
        public string Name { get; set; } = default!;
    }

    public class ProvinceUpdate {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
