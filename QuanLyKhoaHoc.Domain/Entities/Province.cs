namespace QuanLyKhoaHoc.Domain.Entities
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<District> Districts { get; set; } = default!;

        public ICollection<User> Users { get; set; } = default!;
    }
}
