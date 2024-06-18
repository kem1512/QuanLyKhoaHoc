namespace QuanLyKhoaHoc.Domain.Entities
{
    public class BillStatus
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<Bill> Bills { get; set; } = default!;
    }
}
