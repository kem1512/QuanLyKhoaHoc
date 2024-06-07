namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Ward
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string Name { get; set; } = default!;

        public District District { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
    }
}
