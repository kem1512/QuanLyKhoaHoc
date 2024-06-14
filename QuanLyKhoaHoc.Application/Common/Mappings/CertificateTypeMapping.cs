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
        [Required(ErrorMessage = "Tên Loại Chứng Chỉ Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }

    public class CertificateTypeUpdate
    {
        [Required(ErrorMessage = "Loại Chứng Chỉ Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Loại Chứng Chỉ Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;
    }
}
