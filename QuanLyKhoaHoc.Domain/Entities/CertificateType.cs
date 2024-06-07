namespace QuanLyKhoaHoc.Domain.Entities
{
    public class CertificateType
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<Certificate> Certificates { get; set; } = default!;
    }
}
