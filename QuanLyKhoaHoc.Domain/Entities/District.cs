namespace QuanLyKhoaHoc.Domain.Entities
{
    public class District
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string Name { get; set; } = default!;

        public Province Province { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;

        public ICollection<Ward> Wards { get; set; } = default!;
    }
}
