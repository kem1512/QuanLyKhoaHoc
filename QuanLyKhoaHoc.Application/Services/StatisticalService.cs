namespace QuanLyKhoaHoc.Application.Services
{
    public class CourseRevenueDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public decimal TotalRevenue { get; set; }
        public int NumberOfPurchases { get; set; }
    }

    public class StatisticalService : IStatisticalService
    {
        private readonly IApplicationDbContext _context;

        public StatisticalService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> TotalRevenueAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Bills.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(b => b.CreateTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(b => b.CreateTime <= endDate.Value);
            }

            return await query.SumAsync(b => b.Price);
        }

        public async Task<ICollection<CourseRevenueDto>> TopSellingCoursesAsync(DateTime? startDate, DateTime? endDate, int topN)
        {
            var query = _context.Courses.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(c => c.Bills.Any(b => b.CreateTime >= startDate.Value && b.CreateTime <= endDate.Value));
            }

            var courses = await query
                .OrderByDescending(c => c.NumberOfPurchases)
                .Take(topN)
                .Select(c => new CourseRevenueDto
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    TotalRevenue = c.Bills.Where(b => b.CreateTime >= startDate && b.CreateTime <= endDate).Sum(b => b.Price),
                    NumberOfPurchases = c.Bills.Count(b => b.CreateTime >= startDate && b.CreateTime <= endDate)
                })
                .ToListAsync();

            return courses;
        }
    }
}
