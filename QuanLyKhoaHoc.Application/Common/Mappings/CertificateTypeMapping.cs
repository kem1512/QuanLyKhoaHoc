using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CertificateTypeMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    public class CertificateTypeQuery : QueryModel { }

    public class CertificateTypeCreate
    {
        public string Name { get; set; } = default!;
    }

    public class CertificateTypeUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
