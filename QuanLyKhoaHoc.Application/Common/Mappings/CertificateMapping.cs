using QuanLyKhoaHoc.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class CertificateMapping
    {
        public int Id { get; set; }

        public int CertificateTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }

    public class CertificateQuery : QueryModel { }

    public class CertificateCreate
    {
        public int CertificateTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }

    public class CertificateUpdate
    {
        public int Id { get; set; }

        public int CertificateTypeId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }
}
