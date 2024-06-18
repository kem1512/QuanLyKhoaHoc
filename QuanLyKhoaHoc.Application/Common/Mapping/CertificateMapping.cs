namespace QuanLyKhoaHoc.Application.Common.Mapping
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
        [Required(ErrorMessage = "Loại Chứng Chỉ Không Được Bỏ Trống")]
        public int CertificateTypeId { get; set; }

        [Required(ErrorMessage = "Tên Chứng Chỉ Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }

    public class CertificateUpdate
    {
        [Required(ErrorMessage = "Chứng Chỉ Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Loại Chứng Chỉ Không Được Bỏ Trống")]
        public int CertificateTypeId { get; set; }

        [Required(ErrorMessage = "Tên Chứng Chỉ Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Image { get; set; } = default!;
    }
}
